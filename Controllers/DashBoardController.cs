using Microsoft.AspNetCore.Mvc;

namespace ThemeIntegrationInNet.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
