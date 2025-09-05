namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using mymvc.Models;
    public class Bai3Controller : Controller
    {
        [HttpGet]
        public ActionResult Bai3()
        {
            return View(new Bai3());
        }

        [HttpPost]
        public ActionResult Bai3(Bai3 model)
        {
            if (model.Height > 0)
            {
                // Chuyển cm -> m
                double heightInMeters = model.Height / 100.0;

                // Tính BMI
                model.BMI = model.Weight / (heightInMeters * heightInMeters);

                // Phân loại theo chỉ số BMI
                if (model.BMI < 18.5)
                    model.Category = "Gầy";
                else if (model.BMI < 24.9)
                    model.Category = "Bình thường";
                else if (model.BMI < 29.9)
                    model.Category = "Thừa cân";
                else
                    model.Category = "Béo phì";
            }
            else
            {
                ModelState.AddModelError("Height", "Chiều cao phải lớn hơn 0");
            }

            return View(model);
        }
    }
}