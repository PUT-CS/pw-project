using Barski_Lewandowski_Core;
using Barski_Lewandowski_Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barski_Lewandowski_DAOMock.BO
{
    public class Broom : IBroom
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Nazwa nie może być dłuższa niż 50 znaków")]
        public string Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Cena musi być liczbą dodatnią.")]
        public double Price { get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
        public HandleMaterialType HandleMaterial { get; set; }
        public FibersMaterialType FibersMaterial { get; set; }

        [NotMapped]
        IProducer IBroom.Producer
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
