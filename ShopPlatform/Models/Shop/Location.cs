using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ShopPlatform.Models.Shop
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }
        public string CountryCode { get; set; }
        public string LocationName { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public Location(JToken jsonToken)
        {
            this.Id = Guid.NewGuid();
            this.CountryCode = jsonToken["country"].Value<string>();
            this.LocationName = jsonToken["name"].Value<string>();
            this.Latitude = Double.Parse(jsonToken["lat"].Value<string>());
            this.Longtitude = Double.Parse(jsonToken["lng"].Value<string>());
        }
        public Location()
        {

        }
    }
}
