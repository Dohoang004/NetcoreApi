namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using mymvc.Models;

    public class EmployeeController : Controller
    {
        public IActionResult employee()
        {
            return View();
        }

        public IActionResult employee2()
        {

            return View();
        }
        [HttpPost]

        public IActionResult employee2(string email, Employee ep)
        {
            ViewBag.emailandname = email + " - " + ep.FullName+" - "+ep.Address;
            return View();
        }
        
    }
}