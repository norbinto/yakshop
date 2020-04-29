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

namespace YakShop.Application.Herds
{
    public class Update
    {
        public class Command : IRequest
        {
            public List<Yak> Yaks { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Reservations.RemoveRange(_context.Reservations.ToArray());
                _context.Order.RemoveRange(_context.Order.ToArray());
                _context.Yaks.RemoveRange(_context.Yaks.ToArray());
                _context.Yaks.AddRange(request.Yaks);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}
