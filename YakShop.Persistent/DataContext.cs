using Microsoft.EntityFrameworkCore;
using System;
using YakShop.Domain;

namespace YakShop.Persistent
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Yak> Yaks { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Order> Order { get; set; }
    }
}
