using System.ComponentModel.DataAnnotations;
using CustomerPlatform.Core.Models.Base;

namespace CustomerPlatform.Core.Models.Customers
{
    public class MrGreenCustomerDto : CustomerDtoBase
    {
        [Required]
        public string PersonalNumber { get; set; }
    }
}
