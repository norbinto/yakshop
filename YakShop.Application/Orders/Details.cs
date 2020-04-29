using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YakShop.Domain;
using YakShop.Persistent;

namespace YakShop.Application.Orders
{
    public class Details
    {
        public class Query : IRequest<List<Reservation>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Reservation>>
        {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Reservation>> Handle(Query request, CancellationToken cancellationToken)
            {
                var reservations = await _context.Reservations.Include(x => x.Order).ToListAsync();

                return reservations;
            }
        }
    }
}

