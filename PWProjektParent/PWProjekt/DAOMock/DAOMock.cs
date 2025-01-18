using Milek_Nowak_Interfaces;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Milek_Nowak_DAOMock
{
    public class DAOMock: IDAO
    {
        private int _nextGameID = 1;
        private int _nextProducerID = 1;
        public List<BO.Producer> Producers { get; set; }
        public List<BO.Game> Games { get; set; }

        public DAOMock()
        { 
            Producers = new List<BO.Producer>();
            BO.Producer nentendo = new BO.Producer();
            nentendo.Id = _nextProducerID++;
            nentendo.Name = "Nentendo";
            nentendo.PhoneNumber = "733 797 268";
            nentendo.Country = "Japan";
            Producers.Add(nentendo);
            BO.Producer cdprojektgreen = new BO.Producer();
            cdprojektgreen.Id = _nextProducerID++;
            cdprojektgreen.Name = "CD Projekt Green";
            cdprojektgreen.PhoneNumber = "213 742 555";
            cdprojektgreen.Country = "Poland";
            Producers.Add(cdprojektgreen);
            BO.Producer triangleenix = new BO.Producer();
            triangleenix.Id = _nextProducerID++;
            triangleenix.Name = "Triangle Enix";
            triangleenix.PhoneNumber = "333 444 555";
            triangleenix.Country = "Japan";
            Producers.Add(triangleenix);
            Games = new List<BO.Game>();
            BO.Game game = new BO.Game();
            game.Name = "Super Smash Sisters";
            game.Id = _nextGameID++;
            game.GameType = Milek_Nowak_Core.GameType.Action;
            game.ProducerId = nentendo.Id;
            game.Producer = nentendo;
            game.GameTheme = Milek_Nowak_Core.GameTheme.Fantasy;
            game.Price = 59.99;
            Games.Add(game);
            game = new BO.Game();
            game.Name = "Super Mario Bike 8";
            game.Id = _nextGameID++;
            game.GameType = Milek_Nowak_Core.GameType.Action;
            game.ProducerId = nentendo.Id;
            game.Producer = nentendo;
            game.GameTheme = Milek_Nowak_Core.GameTheme.Fantasy;
            game.Price = 99.99;
            Games.Add(game);
            game = new BO.Game();
            game.Name = "Der Hexer";
            game.GameType = Milek_Nowak_Core.GameType.Strategy;
            game.Id = _nextGameID++;
            game.ProducerId = cdprojektgreen.Id;
            game.Producer = cdprojektgreen;
            game.GameTheme = Milek_Nowak_Core.GameTheme.Medieval;
            game.Price = 9.99;
            Games.Add(game);
            game = new BO.Game();
            game.Name = "Deus Ex";
            game.Id = _nextGameID++;
            game.GameType = Milek_Nowak_Core.GameType.FPS;
            game.ProducerId = triangleenix.Id;
            game.Producer = triangleenix;
            game.GameTheme = Milek_Nowak_Core.GameTheme.Cyberpunk;
            game.Price = 19.99;
            Games.Add(game);
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            p.Id = _nextProducerID++;
            Producers.Add(p);
        }

        public void AddGame(IGame game)
        {
            BO.Game p = game as BO.Game;
            p.Id = _nextGameID++ ;
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
            return Games;
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
            return;
        }

        public void UndoChanges()
        {
            return;
        }
    }
}

