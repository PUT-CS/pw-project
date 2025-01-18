using Milek_Nowak_Core;
using Milek_Nowak_Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Milek_Nowak_DAOEF.BO
{
    public class Game : IGame
    {
        public int Id { get; set; }
        [StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
        public GameTheme GameTheme { get; set; }
        public GameType GameType { get; set; }

        [NotMapped]
        IProducer IGame.Producer
        {
            get => Producer;
            set
            {
                Producer = value as Producer;
                ProducerId = value.Id;
            }
        }
    }
}
