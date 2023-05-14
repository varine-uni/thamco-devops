using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ThAmCo.Web.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly IConfiguration _configuration;

        public ManageUserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public IActionResult ManageUser()
        {
            string role = _configuration["Auth0:Audience"] + "roles";

            if (User.HasClaim(c => c.Type == role && c.Value == "Manager"))
            {
                return View();
            }

            return Forbid();
        }
    }
}
