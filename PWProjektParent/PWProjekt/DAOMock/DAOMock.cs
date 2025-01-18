using Milek_Nowak_Interfaces;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Milek_Nowak_DAOMock
{
    public class DAOMock: IDAO
    {
        private int _nextBroomID = 1;
        private int _nextProducerID = 1;
        public List<BO.Producer> Producers { get; set; }
        public List<BO.Broom> Brooms { get; set; }

        public DAOMock()
        { 
            Producers = new List<BO.Producer>();
            BO.Producer miotlopol = new BO.Producer();
            miotlopol.Id = _nextProducerID++;
            miotlopol.Name = "MITŁOPOL";
            miotlopol.PhoneNumber = "111 111 123";
            miotlopol.Country = "Polska";
            Producers.Add(miotlopol);
            BO.Producer miotlex = new BO.Producer();
            miotlex.Id = _nextProducerID++;
            miotlex.Name = "MIOTŁEX";
            miotlex.PhoneNumber = "111 111 124";
            miotlex.Country = "Polska";
            Producers.Add(miotlex);
            BO.Producer miotloplast = new BO.Producer();
            miotloplast.Id = _nextProducerID++;
            miotloplast.Name = "MITŁOPLAST";
            miotloplast.PhoneNumber = "111 111 125";
            miotloplast.Country = "Polska";
            Producers.Add(miotloplast);
            Brooms = new List<BO.Broom>();
            BO.Broom broom = new BO.Broom();
            broom.Name = "Super Broom 5000";
            broom.Id = _nextBroomID++;
            broom.FibersMaterial = Milek_Nowak_Core.FibersMaterialType.Słoma;
            broom.ProducerId = miotlopol.Id;
            broom.Producer = miotlopol;
            broom.HandleMaterial = Milek_Nowak_Core.HandleMaterialType.Aluminium;
            broom.Price = 59.99;
            Brooms.Add(broom);
            broom = new BO.Broom();
            broom.Name = "Ultra Broom 9000";
            broom.Id = _nextBroomID++;
            broom.FibersMaterial = Milek_Nowak_Core.FibersMaterialType.Włosie;
            broom.ProducerId = miotlopol.Id;
            broom.Producer = miotlopol;
            broom.HandleMaterial = Milek_Nowak_Core.HandleMaterialType.Stal;
            broom.Price = 99.99;
            Brooms.Add(broom);
            broom = new BO.Broom();
            broom.Name = "Broom Broom";
            broom.FibersMaterial = Milek_Nowak_Core.FibersMaterialType.Słoma;
            broom.Id = _nextBroomID++;
            broom.ProducerId = miotlex.Id;
            broom.Producer = miotlex;
            broom.HandleMaterial = Milek_Nowak_Core.HandleMaterialType.Drewno;
            broom.Price = 9.99;
            Brooms.Add(broom);
            broom = new BO.Broom();
            broom.Name = "Miotła codzienna";
            broom.Id = _nextBroomID++;
            broom.FibersMaterial = Milek_Nowak_Core.FibersMaterialType.Sztuczne;
            broom.ProducerId = miotloplast.Id;
            broom.Producer = miotloplast;
            broom.HandleMaterial = Milek_Nowak_Core.HandleMaterialType.Plastik;
            broom.Price = 19.99;
            Brooms.Add(broom);
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            p.Id = _nextProducerID++;
            Producers.Add(p);
        }

        public void AddBroom(IBroom broom)
        {
            BO.Broom p = broom as BO.Broom;
            p.Id = _nextBroomID++ ;
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
            return Brooms;
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
            return;
        }

        public void UndoChanges()
        {
            return;
        }
    }
}

