using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milek_Nowak_Interfaces;
using Milek_Nowak_Core;
using Newtonsoft.Json.Linq;

namespace Milek_Nowak_WebApp.Controllers
{
    public class BroomsController : Controller
    {
        private IDAO _dao;

        public BroomsController(Milek_Nowak_BLC.BLC blc)
        {
            _dao = blc.DAO;
        }

        // GET: Brooms
        public async Task<IActionResult> Index(string searchQuery)
        {
            ViewData["SearchQuery"] = searchQuery;

            var brooms = _dao.GetAllBrooms();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                brooms = brooms.Where(b => b.Name.Contains(searchQuery) || b.Producer.Name.Contains(searchQuery));
            }

            return View(brooms);
        }

        // GET: Brooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var broom = _dao.GetAllBrooms().FirstOrDefault(p => p.Id == id);
            if (broom == null)
            {
                return NotFound();
            }

            return View(broom);
        }

        // GET: Brooms/Create
        public IActionResult Create()
        {
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View();
        }

        // POST: Brooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string name, double price, int producerId, int handleMaterial, int fibersMaterial)
        {
            IBroom broom = _dao.CreateNewBroom();
            broom.Id = id;
            broom.Name = name;
            broom.Price = Math.Round(price, 2);
            broom.Producer = _dao.GetAllProducers().First(p => p.Id == producerId);
            broom.HandleMaterial = (GameTheme)handleMaterial;
            broom.FibersMaterial = (GameType)fibersMaterial;

            ModelState.Clear();
            TryValidateModel(broom);

            if (ModelState.IsValid)
            {
                _dao.AddBroom(broom);
                _dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View(broom);
        }

        // GET: Brooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var broom = _dao.GetAllBrooms().FirstOrDefault(p => p.Id == id);
            if (broom == null)
            {
                return NotFound();
            }
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View(broom);
        }

        // POST: Brooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, double price, int producerId, int handleMaterial, int fibersMaterial)
        {
            IBroom broom = _dao.GetAllBrooms().FirstOrDefault(p => p.Id == id);
            IBroom broomTmp = _dao.CreateNewBroom();

            if (id != broom.Id)
            {
                return NotFound();
            }

            broomTmp.Id = id;
            broomTmp.Name = name;
            broomTmp.Price = Math.Round(price, 2);
            broomTmp.Producer = _dao.GetAllProducers().First(p => p.Id == producerId);
            broomTmp.HandleMaterial = (GameTheme)handleMaterial;
            broomTmp.FibersMaterial = (GameType)fibersMaterial;

            ModelState.Clear();
            TryValidateModel(broomTmp);

            if (ModelState.IsValid)
            {
                broom.Name = broomTmp.Name;
                broom.Producer = broomTmp.Producer;
                broom.HandleMaterial = broomTmp.HandleMaterial;
                broom.FibersMaterial = broomTmp.FibersMaterial;
                broom.Price = broomTmp.Price;

                try
                {
                    _dao.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BroomExists(broom.Id))
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
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View(broom);
        }

        // GET: Brooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var broom = _dao.GetAllBrooms().FirstOrDefault(p => p.Id == id);
            if (broom == null)
            {
                return NotFound();
            }

            return View(broom);
        }

        // POST: Brooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var broom = _dao.GetAllBrooms().FirstOrDefault(p => p.Id == id);

            if (broom != null)
            {
                _dao.RemoveBroom(broom);
            }

            _dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool BroomExists(int id)
        {
            return _dao.GetAllBrooms().Any(p => p.Id == id);
        }
    }
}
