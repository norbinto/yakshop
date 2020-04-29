using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YakShop.Domain;
using YakShop.Persistent;

namespace YakShop.Application.Stocks
{
    public class Details
    {
        public class Query : IRequest<Stock>
        {
            public int Days { get; set; }
        }

        public class Handler : IRequestHandler<Query, Stock>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Stock> Handle(Query request, CancellationToken cancellationToken)
            {
                var reservations = await _context.Reservations.Include(x=>x.Order).Where(x => true).ToListAsync();
                var yaks = await _context.Yaks.Where(x => true).ToListAsync();

                var ret = new Stock(yaks, reservations, request.Days);

                return ret;
            }
        }
    }
}
