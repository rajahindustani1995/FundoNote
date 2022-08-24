using Microsoft.AspNetCore.Mvc;

namespace FundoNote.Controllers
{
    public class NotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
