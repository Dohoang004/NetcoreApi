namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;

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
        public IActionResult employee2(string email, string name)
        {
            ViewBag.emailandname = email + " - " + name;
            return View();
        }
        
    }
}