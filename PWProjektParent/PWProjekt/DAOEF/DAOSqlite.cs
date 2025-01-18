using Milek_Nowak_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Milek_Nowak_DAOEF
{
    public class DAOSqlite : DbContext, IDAO
    {
        public DbSet<BO.Producer> Producers { get; set; }
        public DbSet<BO.Game> Games { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=C:\Users\sebno.DESKTOP-CP5LO9F\Desktop\pw-project\PWProjektParent\PWProjekt\DAOEF\games.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BO.Game>()
                .HasOne(c => c.Producer)
                .WithMany(p => p.Games)
                .HasForeignKey(c => c.ProducerId)
                .IsRequired();
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            Producers.Add(p);
        }

        public void AddGame(IGame game)
        {
            BO.Game p = game as BO.Game;
            Games.Add(p);
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IGame CreateNewGame()
        {
            return new BO.Game();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public IEnumerable<IGame> GetAllGames()
        {
            return Games.Include("Producer").ToList();
        }

        public void RemoveProducer(IProducer producer)
        {
            Producers.Remove(producer as BO.Producer);
        }

        public void RemoveGame(IGame game)
        {
            Games.Remove(game as BO.Game);
        }

        public void UpdateProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public void UpdateGame(IGame game)
        {
            throw new NotImplementedException();
        }

        void IDAO.SaveChanges()
        {
            SaveChanges();
        }

        public void UndoChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                    entry.State = EntityState.Unchanged;
                SaveChanges();
            }
        }
    }
}
