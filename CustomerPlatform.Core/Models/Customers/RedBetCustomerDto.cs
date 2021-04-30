using System.ComponentModel.DataAnnotations;

namespace CustomerPlatform.Core.Models.Customers
{
    public class RedBetCustomerDto : CustomerDtoBase
    {
        [Required]
        public string FavoriteFootballTeam { get; set; }
    }
}
