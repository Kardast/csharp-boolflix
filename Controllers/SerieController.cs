using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace csharp_boolflix.Controllers
{
    [Authorize]

    public class SerieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
