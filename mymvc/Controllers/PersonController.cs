
using mymvc.Data;

namespace mymvc.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using mymvc.Models;
    using X.PagedList;
    using X.PagedList.Extensions;


    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }
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
        public async Task<IActionResult> Index(int? page) //index() : //tên file cshtml trong view
        {
            var model =  _context.Person.ToList().ToPagedList(page ?? 1, 5);
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
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address,Country")] Person person)//tên file cshtml trong view
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
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address,Country")] Person person)//tên file cshtml trong view
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









