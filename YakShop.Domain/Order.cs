using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace YakShop.Domain
{
    [Serializable]

    public class Order
    {
        public Guid Id { get; set; }
        private double _milk;
        public double Milk { get { return Math.Round(_milk, 2); } set { _milk = value; } }

        public int Skins { get; set; }

        public bool ShouldSerializeMilk()
        {
            return (Milk > 0.0);
        }

        public bool ShouldSerializeSkins()
        {
            return (Skins > 0);
        }

        public bool ShouldSerializeId()
        {
            return false;
        }

    }
}