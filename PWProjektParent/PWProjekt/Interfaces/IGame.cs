using Milek_Nowak_Core;

namespace Milek_Nowak_Interfaces
{
    public interface IGame
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
        IProducer Producer { get; set; }
        GameTheme GameTheme { get; set; }
        GameType GameType { get; set; }
    }
}
