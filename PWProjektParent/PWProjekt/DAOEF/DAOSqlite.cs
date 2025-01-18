using Barski_Lewandowski_Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Barski_Lewandowski_DAOEF
{
    public class DAOSqlite : DbContext, IDAO
    {
        public DbSet<BO.Producer> Producers { get; set; }
        public DbSet<BO.Broom> Brooms { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO: do pliku?????????
            optionsBuilder.UseSqlite(@"Filename=C:\Users\sebno.DESKTOP-CP5LO9F\Desktop\Projekt-Wizualne\DAOEF\brooms.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BO.Broom>()
                .HasOne(c => c.Producer)
                .WithMany(p => p.Brooms)
                .HasForeignKey(c => c.ProducerId)
                .IsRequired();
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            Producers.Add(p);
        }

        public void AddBroom(IBroom broom)
        {
            BO.Broom p = broom as BO.Broom;
            Brooms.Add(p);
        }

        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public IBroom CreateNewBroom()
        {
            return new BO.Broom();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public IEnumerable<IBroom> GetAllBrooms()
        {
            return Brooms.Include("Producer").ToList();
        }

        public void RemoveProducer(IProducer producer)
        {
            Producers.Remove(producer as BO.Producer);
        }

        public void RemoveBroom(IBroom broom)
        {
            Brooms.Remove(broom as BO.Broom);
        }

        public void UpdateProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public void UpdateBroom(IBroom broom)
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
