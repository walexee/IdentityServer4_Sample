using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Web.IDSv4
{
    public class UserPasswordValidator : IResourceOwnerPasswordValidator
    {
        //this is used to validate your user account with provided grant at /connect/token
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                //get your user model from db (by username - in my case its email)
                var user = GetUser(context.UserName);
                if (user != null)
                {
                    //check if password match - remember to hash password if stored as hash in db
                    if (user.Password == context.Password)
                    {
                        var userProfile = GetUserProfile(user.UserId);

                        //set the result
                        context.Result = new GrantValidationResult(
                            subject: user.UserId.ToString(),
                            authenticationMethod: context.Request.GrantType, // . "custom",
                            claims: GetUserClaims(userProfile, new string[] { "Admin", "GoodUser" }));

                        return;
                    }

                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                    return;
                }
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }
            catch (Exception ex)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            }
        }

        //build claims array from user data
        public static IEnumerable<Claim> GetUserClaims(UserProfile user, string[] roles)
        {
            var claims = new List<Claim>
            {
                new Claim("user_id", user.UserId.ToString()),
                new Claim(JwtClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(JwtClaimTypes.GivenName, user.FirstName),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.Email, user.Email)
            };

            //roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, role));
            }

            return claims;
        }

        // temp method
        public static UserLoginInfo GetUser(string userName)
        {
            return new UserLoginInfo { UserId = Guid.NewGuid(), UserName = userName, Password = "password" };
        }

        public static UserProfile GetUserProfile(Guid userId)
        {
            return new UserProfile { UserId = userId };
        }

        public static UserProfile GetUserProfileByName(string name)
        {
            return new UserProfile { UserId = Guid.NewGuid(), FirstName = name };
        }
    }

    public class UserLoginInfo
    {
        public Guid UserId { get; set; }

        public Guid TenantId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class UserProfile
    {
        public Guid UserId { get; set; }

        public bool IsActive { get; set; }

        public Guid TenantId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
