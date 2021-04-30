using System.ComponentModel.DataAnnotations;

namespace CustomerPlatform.Core.Models
{
    public class AddressDto
    {
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}