using Microsoft.AspNetCore.Mvc;

namespace ThAmCo.Web.Controllers
{
    public class ManageUserController : Controller
    {
        public IActionResult ManageUser()
        {
            return View();
        }
    }
}
