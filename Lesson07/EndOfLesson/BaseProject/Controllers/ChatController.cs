using Microsoft.AspNetCore.Mvc;

namespace BaseProject.Controllers
{
    [Route("Chat")]
    public class ChatController : Controller
    {
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}