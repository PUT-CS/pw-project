using Milek_Nowak_Interfaces;
using Milek_Nowak_Core;
using System.Configuration;

namespace Milek_Nowak_ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            IDAO dao = Milek_Nowak_BLC.BLC.GetInstance(libraryName).DAO;

            foreach (IProducer p in dao.GetAllProducers())
            {
                dao.RemoveProducer(p);
            }
            foreach(IGame p in dao.GetAllGames())
            {
                dao.RemoveGame(p);
            }

            IProducer nentendo = dao.CreateNewProducer();
            nentendo.Name = "Nentendo";
            nentendo.Country = "Japan";
            nentendo.PhoneNumber = "+03 1234-5678";
            dao.AddProducer(nentendo);

            IGame sms = dao.CreateNewGame();
            sms.Name = "Super Mario Sisters";
            sms.Price = 49.99;
            sms.Producer = nentendo;
            sms.GameTheme = GameTheme.Western;
            dao.AddGame(sms);

            IGame sekiro = dao.CreateNewGame();
            sekiro.Name = "Sekiro: Shadows Do Not Die At All";
            sekiro.Price = 59.99;
            sekiro.Producer = nentendo;
            sekiro.GameTheme = GameTheme.Fantasy;
            sekiro.GameType = GameType.Strategy;
            dao.AddGame(sekiro);

            IProducer cdp = dao.CreateNewProducer();
            cdp.Name = "CD Projekt Green";
            cdp.Country = "Poland";
            dao.AddProducer(cdp);

            IGame wiedzmin = dao.CreateNewGame();
            wiedzmin.Name = "Der Hexer 3";
            wiedzmin.Price = 5.00;
            wiedzmin.Producer = cdp;
            wiedzmin.GameTheme = GameTheme.Cyberpunk;
            wiedzmin.GameType = GameType.Action;
            dao.AddGame(wiedzmin);

            IGame cyberpunk = dao.CreateNewGame();
            cyberpunk.Name = "Cyberpunk 2222";
            cyberpunk.Producer = cdp;
            cyberpunk.Price = 39.99;
            cyberpunk.GameTheme = GameTheme.Medieval;
            cyberpunk.GameType = GameType.FPS;
            dao.AddGame(cyberpunk);

            dao.SaveChanges();

            Console.WriteLine("ALL PRODUCERS:");
            foreach (IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Name}");
            }

            Console.WriteLine("\nALL GAMES:");
            foreach (IGame c in dao.GetAllGames())
            {
                Console.WriteLine($"{c.Name}, {c.Producer.Name}");
            }
        }
    }
}
