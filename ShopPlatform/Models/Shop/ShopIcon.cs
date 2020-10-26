using System;
using System.ComponentModel.DataAnnotations;

namespace ShopPlatform.Models.Shop
{
    public class ShopIcon: IIcon<Shop>
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public Shop Reference { get; set; }
        public DateTime DateAdded { get; set; }
    }
}