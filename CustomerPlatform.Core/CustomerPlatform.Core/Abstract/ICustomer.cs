using System.ComponentModel.DataAnnotations;
using CustomerPlatform.Core.Models;

namespace CustomerPlatform.Core.Abstract
{
    public interface ICustomer
    {
        int Id { get; set; }
        [Required]
        string FirstName { get; set; }
        [Required]
        string LastName { get; set; }
        [Required]
        string CustomerType { get; set; }
        [Required]
        AddressDto Address { get; set; }
    }
}