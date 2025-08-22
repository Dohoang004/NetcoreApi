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
    }
}