using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace YakShop.Domain
{
    [Serializable]
    public class Reservation
    {
        public Guid Id { get; set; }
        public Order Order { get; set; }
        public int Day { get; set; }
        public string Customer { get; set; }
    }
}
