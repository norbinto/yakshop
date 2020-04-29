using System.Collections.Generic;
using YakShop.Domain;

namespace YakShop.API.Models
{
    public class HerdViewModel
    {
        public List<YakViewModel> Herd { get; set; }

        public HerdViewModel(List<Yak> yaks, int days)
        {
            Herd = new List<YakViewModel>();
            foreach (var yak in yaks)
            {
                if (yak.Age < 10.0)
                {
                    Herd.Add(new YakViewModel
                    {
                        Name = yak.Name,
                        Age = yak.Age,
                        AgeLastShaved = yak.GetLastShave(days)
                    });
                }
            }
        }
    }
}