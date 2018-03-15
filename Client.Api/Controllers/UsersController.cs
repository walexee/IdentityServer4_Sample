using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using System.Net.Http;

namespace Client.Api.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : Controller
    {
        public UserInfo[] Index()
        {
            return new UserInfo[]
            {
                new UserInfo
                {
                    Name = User.Identity.Name,
                    Id = Guid.NewGuid()
                }
            };
        }

        
    }

    public class UserInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}