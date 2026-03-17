using Microsoft.AspNetCore.Mvc;

namespace DH_ApiGateway.Controllers
{
    public class HealthCheckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
