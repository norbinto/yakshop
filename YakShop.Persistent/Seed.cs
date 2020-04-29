using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YakShop.Domain;

namespace YakShop.Persistent
{
   public  class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.Yaks.Any())
            {
                var moments = new List<Yak> {
                    new Yak(4.0,Sex.female,"Betty-1"),
                    new Yak(8.0,Sex.female,"Betty-2"),
                    new Yak(9.5,Sex.female,"Betty-3")
                };
                context.Yaks.AddRange(moments);
                context.SaveChanges();
            }
        }
    }
}
