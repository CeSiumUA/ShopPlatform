using System;
using System.ComponentModel.DataAnnotations;

namespace ShopPlatform.Models
{
    public interface IIcon<T>
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public T Reference { get; set; }
    }
}