using Milek_Nowak_Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Milek_Nowak_DAOEF.BO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        [StringLength(50, ErrorMessage = "Country name cannot be longer than 50 characters")]
        public string Country { get; set; } = string.Empty;
        [RegularExpression(@"^\+?[0-9\s]*$", ErrorMessage = "Telephone number can only contain digits, spaces and can begin from a plus")]
        [StringLength(25, ErrorMessage = "Telephone number cannot be longer than 25 characters")]
        public string PhoneNumber { get; set; } = "+48 123 456 789";

        public List<Game> Games;
    }
}
