using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapTest.Data;
using MapTest.Models;

namespace MapTest.Controllers
{
    public class FederalSubjectsController : Controller
    {
        private readonly EFCoreTestContext _context;

        public FederalSubjectsController(EFCoreTestContext context)
        {
            _context = context;
        }

        // GET: FederalSubjects
        public async Task<IActionResult> Index()
        {
            var eFCoreTestContext = _context.FederalSubjects.Include(f => f.FederalDistrict);
            return View(await eFCoreTestContext.ToListAsync());
        }

        // GET: FederalSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var federalSubject = await _context.FederalSubjects
                .Include(f => f.FederalDistrict)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (federalSubject == null)
            {
                return NotFound();
            }

            return View(federalSubject);
        }

        // GET: FederalSubjects/Create
        public IActionResult Create()
        {
            ViewData["FederalDistrictID"] = new SelectList(_context.FederalDistricts, "ID", "Name");
            return View();
        }

        // POST: FederalSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,FederalDistrictID")] FederalSubject federalSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(federalSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FederalDistrictID"] = new SelectList(_context.FederalDistricts, "ID", "ID", federalSubject.FederalDistrictID);
            return View(federalSubject);
        }

        // GET: FederalSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var federalSubject = await _context.FederalSubjects.FindAsync(id);
            if (federalSubject == null)
            {
                return NotFound();
            }
            ViewData["FederalDistrictID"] = new SelectList(_context.FederalDistricts, "ID", "ID", federalSubject.FederalDistrictID);
            return View(federalSubject);
        }

        // POST: FederalSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,FederalDistrictID")] FederalSubject federalSubject)
        {
            if (id != federalSubject.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(federalSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FederalSubjectExists(federalSubject.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FederalDistrictID"] = new SelectList(_context.FederalDistricts, "ID", "ID", federalSubject.FederalDistrictID);
            return View(federalSubject);
        }

        // GET: FederalSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var federalSubject = await _context.FederalSubjects
                .Include(f => f.FederalDistrict)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (federalSubject == null)
            {
                return NotFound();
            }

            return View(federalSubject);
        }

        // POST: FederalSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var federalSubject = await _context.FederalSubjects.FindAsync(id);
            _context.FederalSubjects.Remove(federalSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FederalSubjectExists(int id)
        {
            return _context.FederalSubjects.Any(e => e.ID == id);
        }
    }
}
