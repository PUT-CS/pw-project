namespace Barski_Lewandowski_Interfaces
{
    public interface IDAO
    {
        IEnumerable<IBroom> GetAllBrooms();
        IBroom CreateNewBroom();
        void AddBroom(IBroom broom);
        void UpdateBroom(IBroom broom);
        void RemoveBroom(IBroom broom);

        IEnumerable<IProducer> GetAllProducers();
        IProducer CreateNewProducer();
        void AddProducer(IProducer producer);
        void UpdateProducer(IProducer producer);
        void RemoveProducer(IProducer producer);

        void SaveChanges();
        void UndoChanges();
    }
}
