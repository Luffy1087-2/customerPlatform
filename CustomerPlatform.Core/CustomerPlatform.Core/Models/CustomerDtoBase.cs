using System.ComponentModel.DataAnnotations;
using CustomerPlatform.Core.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerPlatform.Core.Models
{
    //[BsonIgnoreExtraElements]
    public class CustomerDtoBase : ICustomer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
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
