namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    public class cakeController : Controller
    {
        public IActionResult cake()
        {
            return View();
        }
        [HttpPost]
         public IActionResult cake(string myname)
        {
            ViewBag.print=myname+" PTPMQL";
            return View();
        }
        public IActionResult Welcome()
        {
            ViewData["Message"] = "Your welcome message";

            return View();
        }
    }
}