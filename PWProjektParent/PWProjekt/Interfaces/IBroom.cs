using Barski_Lewandowski_Core;

namespace Barski_Lewandowski_Interfaces
{
    public interface IBroom
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        IProducer Producer { get; set; }
        HandleMaterialType HandleMaterial { get; set; }
        FibersMaterialType FibersMaterial { get; set; }
    }
}
