using Barski_Lewandowski_Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Barski_Lewandowski_DAOMock.BO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Nazwa nie może być dłuższa niż 100 znaków")]
        public string Name { get; set; }
        [StringLength(100, ErrorMessage = "Nazwa kraju nie może być dłuższa niż 100 znaków")]
        public string Country { get; set; }
        [RegularExpression(@"^\+?[0-9\s]*$", ErrorMessage = "Numer telefonu może składać się tylko z cyfr i spacji oraz może zaczynać się od plusa")]
        [StringLength(25, ErrorMessage = "Numer telefonu nie może być dłuższy niż 25 znaków")]
        public string PhoneNumber { get; set; }

        public List<Broom> Brooms;
    }
}
