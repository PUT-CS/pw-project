using Milek_Nowak_Core;

namespace Milek_Nowak_Interfaces
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
