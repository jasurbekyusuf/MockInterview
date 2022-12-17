using Microsoft.AspNetCore.Mvc;

namespace MockInterview.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
