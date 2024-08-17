using Microsoft.AspNetCore.Mvc;
using BlogMainStructure.UI.Areas.Author.Controllers;

namespace BlogMainStructure.UI.Areas.AppUser.Controllers
{
    public class HomeController : AuthorBaseController
    {
       
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
