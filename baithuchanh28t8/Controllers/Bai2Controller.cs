namespace baithuchanh28t8.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using baithuchanh28t8.Models;

    public class Bai2Controller : Controller
    {

        [HttpGet]
        public ActionResult Bai2()
        {
            return View(new Bai2());
        }

        [HttpPost]
        public ActionResult Bai2(Bai2 model)
        {
            switch (model.pheptinh)
            {
                case "cộng":
                    model.ketqua = model.so1 + model.so2;
                    break;
                case "trừ":
                    model.ketqua = model.so1 - model.so2;
                    break;
                case "nhân":
                    model.ketqua = model.so1 * model.so2;
                    break;
                case "chia":
                    model.ketqua = model.so2 != 0 ? model.so1 / model.so2 : (double?)null;
                    break;
            }

            return View(model);
        }
    }
}
