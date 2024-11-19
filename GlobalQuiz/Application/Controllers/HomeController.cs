using Microsoft.AspNetCore.Mvc;

namespace GlobalQuiz.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("Bem-vindo ao GlobalQuiz!");
        }
    }
}
