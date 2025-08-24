namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using mymvc.Models;

    public class PersonController : Controller
    {
        public IActionResult person()
        {
            return View();
        }

        public IActionResult person2()
        {

            return View();
        }
        [HttpPost]
        public IActionResult person(Person ps)
        {
            ViewBag.myps = ps.PersonId + " - " + ps.FirstName + " - " + ps.Address;
            return View();
        }
    }
}