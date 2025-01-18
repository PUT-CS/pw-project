using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milek_Nowak_Interfaces;

namespace Milek_Nowak_WebApp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IDAO _dao;

        public ProducersController(Milek_Nowak_BLC.BLC blc)
        {
            _dao = blc.DAO;
        }

        // GET: Producers
        public async Task<IActionResult> Index(string searchQuery)
        {
            ViewData["SearchQuery"] = searchQuery;

            var producers = _dao.GetAllProducers();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                producers = producers.Where(p => p.Name.Contains(searchQuery));
            }


            return View(producers);
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string name, string country, string phoneNumber)
        {
            IProducer producer = _dao.CreateNewProducer();
            producer.Id = id;
            producer.Name = name;
            producer.PhoneNumber = phoneNumber;
            producer.Country = country;

            ModelState.Clear();
            TryValidateModel(producer);

            if (ModelState.IsValid)
            {
                _dao.AddProducer(producer);
                _dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string country, string phoneNumber)
        {
            IProducer producer = _dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            IProducer producerTmp = _dao.CreateNewProducer();

            if (id != producer.Id)
            {
                return NotFound();
            }

            producerTmp.Name = name;
            producerTmp.Country = country;
            producerTmp.PhoneNumber = phoneNumber;

            ModelState.Clear();
            TryValidateModel(producerTmp);

            if (ModelState.IsValid)
            {
                producer.Name = producerTmp.Name;
                producer.Country = producerTmp.Country;
                producer.PhoneNumber = producerTmp.PhoneNumber;
                try
                {
                    _dao.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.Id))
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
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = _dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = _dao.GetAllProducers().FirstOrDefault(p => p.Id == id);

            if (producer != null)
            {
                _dao.RemoveProducer(producer);
            }

            _dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return _dao.GetAllProducers().Any(p => p.Id == id);
        }
    }
}
