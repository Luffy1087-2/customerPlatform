using System.ComponentModel.DataAnnotations;
using CustomerPlatform.Core.Abstract;

namespace CustomerPlatform.Core.Models
{
    public class CustomerDtoBase : ICustomer
    {
        public int Id { get; set; }
        [Required]
        public string CustomerType { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public AddressDto Address { get; set; }
    }
}
