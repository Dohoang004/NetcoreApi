namespace baithuchanh28t8.Controllers
{

    using Microsoft.AspNetCore.Mvc;

    public class Bai1Controller : Controller
    {
        public IActionResult Bai1()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            ViewData["Message"] = "Your welcome message";

            return View();
        }
        [HttpPost]
        public IActionResult Bai1(string name, int birth)
        {
            int age = 2025 - birth;
            ViewBag.bai1 = "Ho ten: " + name + " - " + age + " tuoi";
            return View();
        }
    }
}