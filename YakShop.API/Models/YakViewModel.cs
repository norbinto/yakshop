using Newtonsoft.Json;
using System;

namespace YakShop.API.Models
{
    public class YakViewModel
    {
        public string Name { get; set; }
        public double Age { get; set; }
        [JsonProperty(PropertyName = "age-last-shaved")]
        private double _ageLastShaved;
        public double AgeLastShaved
        {
            get { return Math.Round(_ageLastShaved, 2); }
            set { _ageLastShaved = value; }
        }
    }
}