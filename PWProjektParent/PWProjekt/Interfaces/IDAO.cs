namespace Milek_Nowak_Interfaces
{
    public interface IDAO
    {
        IEnumerable<IGame> GetAllGames();
        IGame CreateNewGame();
        void AddGame(IGame game);
        void UpdateGame(IGame game);
        void RemoveGame(IGame game);

        IEnumerable<IProducer> GetAllProducers();
        IProducer CreateNewProducer();
        void AddProducer(IProducer producer);
        void UpdateProducer(IProducer producer);
        void RemoveProducer(IProducer producer);

        void SaveChanges();
        void UndoChanges();
    }
}
