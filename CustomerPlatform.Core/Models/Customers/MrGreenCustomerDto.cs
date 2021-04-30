using System.ComponentModel.DataAnnotations;

namespace CustomerPlatform.Core.Models.Customers
{
    public class MrGreenCustomerDto : CustomerDtoBase
    {
        [Required]
        public string PersonalNumber { get; set; }
    }
}
