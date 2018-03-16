using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Web.IDSv4
{
    public class UserProfileService : IProfileService
    {
        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //depending on the scope accessing the user data.
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    //get user from db (in my case this is by email)
                    var user = UserPasswordValidator.GetUserProfileByName(context.Subject.Identity.Name);

                    if (user != null)
                    {
                        var claims = UserPasswordValidator.GetUserClaims(user, new string[] { "User", "Manager" });

                        //set issued claims to return
                        context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                    }
                }
                else
                {
                    //get subject from context (this was set ResourceOwnerPasswordValidator.ValidateAsync),
                    //where and subject was set to my user id.
                    var userIdClaim = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
                    var userId = Guid.Parse(userIdClaim?.Value); // TODO: try parse instead

                    if (userId  != Guid.Empty)
                    {
                        //get user from db (find user by user id)
                        var user = UserPasswordValidator.GetUserProfile(userId);

                        // issue the claims for the user
                        if (user != null)
                        {
                            var claims = UserPasswordValidator.GetUserClaims(user, new string[] { "User", "Manager" });

                            context.IssuedClaims = claims.Where(x => context.RequestedClaimTypes.Contains(x.Type)).ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log your error
            }
        }

        //check if user account is active.
        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                //get subject from context (set in ResourceOwnerPasswordValidator.ValidateAsync),
                var userIdClaim = context.Subject.Claims.FirstOrDefault(x => x.Type == "user_id");
                var userId = Guid.Parse(userIdClaim.Value); // TODO: try parse instead

                if (userId != Guid.Empty)
                {
                    var user = UserPasswordValidator.GetUserProfile(userId);

                    if (user != null)
                    {
                        if (user.IsActive)
                        {
                            context.IsActive = user.IsActive;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error logging
            }
        }
    }
}
