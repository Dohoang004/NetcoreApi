namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;

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
    }
}