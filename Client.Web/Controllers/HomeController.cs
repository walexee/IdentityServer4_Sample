using Client.Web.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Server-to-Server using client credential flow to get resource
        public async Task<IActionResult> GetUserInfo()
        {
            var users = await GetUsers();

            ViewBag.Data = users;

            return View();
        }


        private async Task<string> GetUsers()
        {
            var disco = await DiscoveryClient.GetAsync("https://localhost:44344/");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "webApp", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("userApi");

            var result = default(string);

            if (tokenResponse.IsError)
            {
                result = "Error....could not get access token for API";
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.SetBearerToken(tokenResponse.AccessToken);
                    var response = await client.GetAsync("https://localhost:44377/api/users");

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        result = "Error... could not retrieve users";
                    }
                }
            }

            tokenClient.Dispose();

            return result;
        }
    }
}
