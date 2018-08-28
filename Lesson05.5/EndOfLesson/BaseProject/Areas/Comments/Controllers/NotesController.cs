using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Areas.Comments.Controllers
{
    [Area("Comments")]
    [Route("[area]/Notes")]
    public class NotesController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}