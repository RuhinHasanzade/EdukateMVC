using Microsoft.AspNetCore.Mvc;

namespace EdukateProject.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
