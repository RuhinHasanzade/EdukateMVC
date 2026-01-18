using System.Diagnostics;
using EdukateProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace EdukateProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
