using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YakShop.Domain;
using YakShop.Persistent;

namespace YakShop.Application.Orders
{
    public class Create
    {
        public class Command : IRequest<Order>
        {
            public string Customer { get; set; }
            public Order Order { get; set; }
            public int? Days { get; set; }
        }

        public class Handler : IRequestHandler<Command, Order>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Order> Handle(Command request, CancellationToken cancellationToken)
            {
                var reservations = await _context.Reservations.Where(x => true).Include(x => x.Order).ToListAsync();
                var yaks = await _context.Yaks.Where(x => true).ToListAsync();
                var days = request.Days ?? throw new FormatException("Days can not be null");

                var stock = new Stock(yaks, reservations, days);

                var reserv = new Reservation
                {
                    Id = Guid.NewGuid(),
                    Customer = request.Customer,
                    Day = days,
                    Order = new Order
                    {
                        Id = Guid.NewGuid(),
                        Milk = request.Order.Milk,
                        Skins = request.Order.Skins
                    }
                };

                if (reserv.Order.Milk > stock.Milk)
                {
                    reserv.Order.Milk = 0.0;

                }
                if (reserv.Order.Skins > stock.Skins)
                {
                    reserv.Order.Skins = 0;
                }


                if (reserv.Order.Milk > 0 || reserv.Order.Skins > 0)
                {
                    _context.Reservations.Add(reserv);

                    var success = await _context.SaveChangesAsync() > 0;

                    if (success) return reserv.Order;
                }
                return reserv.Order;
            }
        }


    }
}
