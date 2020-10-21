using System;
using System.ComponentModel.DataAnnotations;

namespace ShopPlatform.Models.Items
{
    public class ItemIcon: IIcon<Item>
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public Item Reference { get; set; }
    }
}