using System;
using System.Collections.Generic;
using System.Text;
using YakShop.Domain;

namespace YakShop.Application
{
    public static class ListsExtensions
    {
        public static string HerdStatusByDaysLater(this List<Yak> yaks, int days)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Yak yak in yaks)
            {
                if (yak.Age * 100 + days >= 1000)
                {
                    sb.Append($"  {yak.Name} has already died\n");
                }
                else
                {
                    sb.Append($"  {yak.Name} {yak.Age + (double)days / 100:0.00} years old\n");
                }
            }

            return sb.ToString();
        }
    }
}
