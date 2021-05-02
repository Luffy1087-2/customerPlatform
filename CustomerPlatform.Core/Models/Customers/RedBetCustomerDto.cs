using System.ComponentModel.DataAnnotations;
using CustomerPlatform.Core.Models.Base;

namespace CustomerPlatform.Core.Models.Customers
{
    public class RedBetCustomerDto : CustomerDtoBase
    {
        [Required]
        public string FavoriteFootballTeam { get; set; }
    }
}
