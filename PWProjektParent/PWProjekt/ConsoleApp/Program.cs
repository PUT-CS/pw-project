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

            Console.WriteLine("Hello World!");

            foreach (IProducer p in dao.GetAllProducers())
            {
                dao.RemoveProducer(p);
            }
            foreach(IGame p in dao.GetAllGames())
            {
                dao.RemoveGame(p);
            }

            IProducer producer1 = dao.CreateNewProducer();
            producer1.Name = "producer 1";
            dao.AddProducer(producer1);

            IGame game1 = dao.CreateNewGame();
            game1.Name = "game 1";
            game1.Producer = producer1;
            game1.GameTheme = GameTheme.Western;
            dao.AddGame(game1);

            IGame game2 = dao.CreateNewGame();
            game2.Name = "game 2";
            game2.Producer = producer1;
            game2.GameTheme = GameTheme.Fantasy;
            dao.AddGame(game2);


            IProducer producer2 = dao.CreateNewProducer();
            producer2.Name = "producer 2";
            dao.AddProducer(producer2);

            IGame game3 = dao.CreateNewGame();
            game3.Name = "game 3";
            game3.Producer = producer2;
            game3.GameTheme = GameTheme.Cyberpunk;
            dao.AddGame(game3);

            IGame game4 = dao.CreateNewGame();
            game4.Name = "game 4";
            game4.Producer = producer2;
            game4.GameTheme = GameTheme.Medieval;
            dao.AddGame(game4);

            dao.SaveChanges();



            Console.WriteLine("** PRODUCERS ** \n");
            foreach (IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id}: {p.Name}");
            }

            Console.WriteLine("\n** gameS ** \n");
            foreach (IGame c in dao.GetAllGames())
            {
                Console.WriteLine($"{c.Id}: {c.Name}, {c.Producer.Name}");
            }

        }
    }
}
