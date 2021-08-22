using DevDe.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevDe.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("error/{id.length(3,3)}")]
        public IActionResult Errors(int id)
        {
            var modelError = new ErrorViewModel();

            if (id == 500)
            {
                modelError.Message = "An error has occurred. Try again later or contact our support";
                modelError.Title = "An error has ocurred";
                modelError.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelError.Message = "The page not exist";
                modelError.Title = "Ops! Page not found";
                modelError.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelError.Message = "You not permission";
                modelError.Title = "Forbidden";
                modelError.ErrorCode = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelError);

        }

    }
}
