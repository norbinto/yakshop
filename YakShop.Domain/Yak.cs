using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace YakShop.Domain
{
    public class Yak
    {
        public Guid Id { get; set; }
        public double Age { get; private set; }
        public Sex Sex { get; private set; }
        public string Name { get; private set; }

        private List<int> _canGetShavedDays { get; set; }

        public Yak(double Age, Sex Sex, string Name)
        {
            this.Age = Age;
            this.Sex = Sex;
            this.Name = Name;
            _canGetShavedDays = CalculateShavingDays();
        }

        public double GetLastShave(int days)
        {
            return Age - ((double)(days - _canGetShavedDays.Where(x => x < days).Max()))/100;
        }

        public double GetMilkByAge(int days)
        {
            return AgeAtDaysLater(days) > 9.999 ? 0 : 50 - (AgeAtDaysLater(days) * 100) * 0.03;
        }

        public int GetSkinsByAge(int days)
        {
            return _canGetShavedDays.Count(x => x < days);
        }

        public void IncreaseAgeByDays(int days)
        {
            Age = AgeAtDaysLater(days);
        }

        public List<int> CalculateShavingDays()
        {
            List<int> ret = new List<int>();
            ret.Add(0);
            double ready = 0.0;
            for (int day = 0; AgeAtDaysLater(day) * 100 < 1000; day++)
            {
                if (ready > 1.0)
                {
                    ret.Add(day);
                    ready = 0.0;
                }
                else
                {
                    ready += 1 / (8 + (Age * 100 + day) * 0.01);
                }
            }
            return ret;
        }

        public double AgeAtDaysLater(int days)
        {
            return Age + (double)days / 100;
        }
    }
}

public enum Sex
{
    male,
    female
}