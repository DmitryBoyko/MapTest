using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapTest.Data;
using MapTest.Models;

namespace MapTest.Controllers
{
    public class FederalDistrictsController : Controller
    {
        private readonly EFCoreTestContext _context;

        public FederalDistrictsController(EFCoreTestContext context)
        {
            _context = context;
        }

        // GET: FederalDistricts
        public async Task<IActionResult> Index()
        {
            return View(await _context.FederalDistricts.ToListAsync());
        }

        // GET: FederalDistricts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var federalDistrict = await _context.FederalDistricts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (federalDistrict == null)
            {
                return NotFound();
            }

            return View(federalDistrict);
        }

        // GET: FederalDistricts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FederalDistricts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,ShortName")] FederalDistrict federalDistrict)
        {
            if (ModelState.IsValid)
            {
                _context.Add(federalDistrict);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(federalDistrict);
        }

        // GET: FederalDistricts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var federalDistrict = await _context.FederalDistricts.FindAsync(id);
            if (federalDistrict == null)
            {
                return NotFound();
            }
            return View(federalDistrict);
        }

        // POST: FederalDistricts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,ShortName")] FederalDistrict federalDistrict)
        {
            if (id != federalDistrict.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(federalDistrict);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FederalDistrictExists(federalDistrict.ID))
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
            return View(federalDistrict);
        }

        // GET: FederalDistricts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var federalDistrict = await _context.FederalDistricts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (federalDistrict == null)
            {
                return NotFound();
            }

            return View(federalDistrict);
        }

        // POST: FederalDistricts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var federalDistrict = await _context.FederalDistricts.FindAsync(id);
            _context.FederalDistricts.Remove(federalDistrict);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FederalDistrictExists(int id)
        {
            return _context.FederalDistricts.Any(e => e.ID == id);
        }
    }
}
