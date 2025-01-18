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
            foreach(IBroom p in dao.GetAllBrooms())
            {
                dao.RemoveBroom(p);
            }

            IProducer producer1 = dao.CreateNewProducer();
            producer1.Name = "producer 1";
            dao.AddProducer(producer1);

            IBroom broom1 = dao.CreateNewBroom();
            broom1.Name = "broom 1";
            broom1.Producer = producer1;
            broom1.HandleMaterial = HandleMaterialType.Drewno;
            dao.AddBroom(broom1);

            IBroom broom2 = dao.CreateNewBroom();
            broom2.Name = "broom 2";
            broom2.Producer = producer1;
            broom2.HandleMaterial = HandleMaterialType.Aluminium;
            dao.AddBroom(broom2);


            IProducer producer2 = dao.CreateNewProducer();
            producer2.Name = "producer 2";
            dao.AddProducer(producer2);

            IBroom broom3 = dao.CreateNewBroom();
            broom3.Name = "broom 3";
            broom3.Producer = producer2;
            broom3.HandleMaterial = HandleMaterialType.Stal;
            dao.AddBroom(broom3);

            IBroom broom4 = dao.CreateNewBroom();
            broom4.Name = "broom 4";
            broom4.Producer = producer2;
            broom4.HandleMaterial = HandleMaterialType.Plastik;
            dao.AddBroom(broom4);

            dao.SaveChanges();



            Console.WriteLine("** PRODUCERS ** \n");
            foreach (IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id}: {p.Name}");
            }

            Console.WriteLine("\n** broomS ** \n");
            foreach (IBroom c in dao.GetAllBrooms())
            {
                Console.WriteLine($"{c.Id}: {c.Name}, {c.Producer.Name}");
            }

        }
    }
}
