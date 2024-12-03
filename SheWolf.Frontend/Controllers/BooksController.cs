using Microsoft.AspNetCore.Mvc;

namespace SheWolf.Frontend.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
