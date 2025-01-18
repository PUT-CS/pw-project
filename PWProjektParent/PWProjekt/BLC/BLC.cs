using Milek_Nowak_Interfaces;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Milek_Nowak_BLC
{
    public class BLC
    {
        private static BLC _instance;
        private static readonly object _lock = new object();

        private IDAO _dao;
        public IDAO DAO => _dao;

        public BLC(IConfiguration configuration)
        {
            string libraryName = configuration.GetValue<string>("LibraryName");
            CreateDao(libraryName);
        }
        private BLC(string libraryName)
        {
            CreateDao(libraryName);
        }

        private void CreateDao(string libraryName)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            Type typeToCreate = null;

            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = t;
                    break;
                }
            }

            _dao = Activator.CreateInstance(typeToCreate) as IDAO;
        }

        public static BLC GetInstance(string libraryName)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new BLC(libraryName);
                }
            }
            return _instance;
        }
    }
}
