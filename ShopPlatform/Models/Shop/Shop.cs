using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ShopPlatform.Models.Accounting;

namespace ShopPlatform.Models.Shop
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string IconUrl { get; set; }
        [JsonIgnore]
        public Account ShopOwner { get; set; }
        public DateTime CreationDate { get; set; }
        public double Rating { get; set; }
        public string MainCategory { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        [NotMapped]
        public (string email, int id) OwnerReference
        {
            get
            {
                return (email: ShopOwner.Email, id: ShopOwner.Id);
            }
        }
    }
}
