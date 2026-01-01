
using mymvc.Data;

namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using mymvc.Models;
    using mymvc.Models.Process;
    using X.PagedList;
    using X.PagedList.Extensions;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Authorization;


    [Authorize]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private ExcelProcess _excelProcess=new ExcelProcess();
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }
        

        //gencode
        private string GenerateNewPersonId()
        {
            // Lấy mã lớn nhất hiện tại trong DB
            var lastPerson = _context.Person
                .OrderByDescending(s => s.PersonId)
                .FirstOrDefault();

            string newId = "PSN001"; // Mặc định cho bản ghi đầu tiên

            if (lastPerson != null)
            {
                // Cắt lấy phần số trong mã, ví dụ STD005 -> 5
                string lastIdNumber = lastPerson.PersonId.Substring(3);
                int nextId = int.Parse(lastIdNumber) + 1;

                // Ghép lại mã mới theo định dạng STD00x
                newId = "PSN" + nextId.ToString("D3"); // D3 = 3 chữ số, thêm 0 phía trước
            }

            return newId;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null)
            {
                string fileExtension=Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Choose a file");
                }
                else
                {
                    var fileName=DateTime.Now.ToShortTimeString()+fileExtension;
                    var filePath=Path.Combine(Directory.GetCurrentDirectory()+"/Uploads/",fileName);
                    var fileLocation=new FileInfo(filePath).ToString();
                    using (var stream=new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //
                        var dt=_excelProcess.ExcelToDataTable(fileLocation);
                        //
                        for(int i = 0; i < dt.Rows.Count; i++)
                        {
                            //
                            var ps=new Person();
                            //
                            ps.PersonId=dt.Rows[i][0].ToString();
                            ps.FullName=dt.Rows[i][1].ToString();
                            ps.Address=dt.Rows[i][2].ToString();
                            //
                            _context.AddAsync(ps);
                        }
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }

        
         public async Task<IActionResult> Index(int? page, int? PageSize) //index() : //tên file cshtml trong view
        {
            // Tạo danh sách các lựa chọn số lượng bản ghi hiển thị
            ViewBag.PageSize = new List<SelectListItem>()
            {
                
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };

            // Xác định số lượng bản ghi trên mỗi trang (mặc định là 5 nếu PageSize null
            int pagesize = (PageSize ?? 5);
            ViewBag.psize = pagesize;

            // Truy vấn dữ liệu và phân trang
            var model =  _context.Person.ToList().ToPagedList(page ?? 1,pagesize);
            return View(model);
        }
        public IActionResult Create()//tên file cshtml trong view
        {
            var newId = GenerateNewPersonId();
    ViewBag.NewPersonId = newId;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)//tên file cshtml trong view
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(person.PersonId))
            person.PersonId = GenerateNewPersonId();
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));//tên file cshtml trong view
            }
            return View(person);
        }
        
                                            // Edit

        public async Task<IActionResult> Edit(string id)//tên file cshtml trong view
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)//tên file cshtml trong view
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));//tên file cshtml trong view
            }
            return View(person);
        }
        
                                    // Delete
        public async Task<IActionResult> Delete(string id)//tên file cshtml trong view
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost, ActionName("Delete")]//tên file cshtml trong view
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deleteconfirmed(string id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Person' is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool PersonExists(string id)
        {
            return ( _context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
        /*public IActionResult person()
        {
            return View();
        }

        public IActionResult person2()
        {

            return View();
        }*/




        /*
        public IActionResult person(Person ps)
        {
            ViewBag.myps = ps.PersonId + " - " + ps.FirstName + " - " + ps.Address;
            return View();
        }
        */









