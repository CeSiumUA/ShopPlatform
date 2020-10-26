using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ShopPlatform.Models.Items
{
    public class Item
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public string Category { get; set; }
        public string ViewId { get; set; }
        public double Rating { get; set; }
        public double Price { get; set; }
        public PriceCurrency Currency { get; set; }
        [JsonIgnore]
        public Shop.Shop Seller { get; set; }
        [NotMapped]
        public List<string> Images { get; set; }
        [NotMapped]
        public int ShopId { get; set; }
        [NotMapped]
        public (string ShopOwnerName, int ShopId) ShopReference
        {
            get
            {
                return (ShopOwnerName: Seller.ShopName, ShopId: Seller.Id);
            }
        }
    }

    public enum PriceCurrency
    {
        USD = 0,
        ILS = 1
    }
}
