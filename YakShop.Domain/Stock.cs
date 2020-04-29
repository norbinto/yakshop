using System;
using System.Collections.Generic;
using System.Text;

namespace YakShop.Domain
{
    public class Stock
    {

        public Stock(List<Yak> yaks, List<Reservation> reservations, int days)
        {
            foreach (Yak yak in yaks)
            {
                for (int i = 0; i < days; i++)
                {
                    Milk += yak.GetMilkByAge(i);
                }
                Skins += yak.GetSkinsByAge(days);
            }

            foreach (var reservation in reservations)
            {
                Milk -= reservation.Order.Milk;
                Skins -= reservation.Order.Skins;
            }
        }

        private double _milk = 0.0;
        public double Milk { get { return _milk < 0.0 ? 0.0 : Math.Round(_milk, 2); } set { _milk = value; } }
        
        private int _skins;
        public int Skins { get { return _skins < 0 ? 0 : _skins; } set { _skins = value; } }

        public override string ToString()
        {
            return $"  {Milk:0.000} liters of milk\n  {Skins:0} skins of Skins";
        }
    }
}
