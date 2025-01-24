using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Milek_Nowak_Interfaces;
using Milek_Nowak_Core;
using Newtonsoft.Json.Linq;

namespace Milek_Nowak_WebApp.Controllers
{
    public class GamesController : Controller
    {
        private IDAO _dao;

        public GamesController(Milek_Nowak_BLC.BLC blc)
        {
            _dao = blc.DAO;
        }

        public async Task<IActionResult> Index(string searchQuery)
        {
            ViewData["SearchQuery"] = searchQuery;

            var games = _dao.GetAllGames();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                games = games.Where(b => b.Name.Contains(searchQuery) || b.Producer.Name.Contains(searchQuery));
            }

            return View(games);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _dao.GetAllGames().FirstOrDefault(p => p.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        public IActionResult Create()
        {
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string name, double price, int producerId, int gameTheme, int gameType)
        {
            IGame game = _dao.CreateNewGame();
            game.Id = id;
            game.Name = name;
            game.Price = Math.Round(price, 2);
            game.Producer = _dao.GetAllProducers().First(p => p.Id == producerId);
            game.GameTheme = (GameTheme)gameTheme;
            game.GameType = (GameType)gameType;

            ModelState.Clear();
            TryValidateModel(game);

            if (ModelState.IsValid)
            {
                _dao.AddGame(game);
                _dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            } else
            {
                Console.WriteLine("Model nie jest walid!");
            }
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View(game);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _dao.GetAllGames().FirstOrDefault(p => p.Id == id);
            if (game == null)
            {
                return NotFound();
            }
            ViewData["Producers"] = new SelectList(_dao.GetAllProducers(), "Id", "Name");
            return View(game);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, double price, int producerId, int gameTheme, int gameType)
        {
            IGame game = _dao.GetAllGames().FirstOrDefault(p => p.Id == id);
            IGame gameTmp = _dao.CreateNewGame();

            if (id != game.Id)
            {
                return NotFound();
            }

            gameTmp.Id = id;
            gameTmp.Name = name;
            gameTmp.Price = Math.Round(price, 2);
            gameTmp.Producer = _dao.GetAllProducers().First(p => p.Id == producerId);
            gameTmp.GameTheme = (GameTheme)gameTheme;
            gameTmp.GameType = (GameType)gameType;

            ModelState.Clear();
            TryValidateModel(gameTmp);

            if (ModelState.IsValid)
            {
                game.Name = gameTmp.Name;
                game.Producer = gameTmp.Producer;
                game.GameTheme = gameTmp.GameTheme;
                game.GameType = gameTmp.GameType;
                game.Price = gameTmp.Price;

                try
                {
                    _dao.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View(game);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = _dao.GetAllGames().FirstOrDefault(p => p.Id == id);
            if (game == null)
            {
                return NotFound();
            }

            return View(game);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = _dao.GetAllGames().FirstOrDefault(p => p.Id == id);

            if (game != null)
            {
                _dao.RemoveGame(game);
            }

            _dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _dao.GetAllGames().Any(p => p.Id == id);
        }
    }
}
