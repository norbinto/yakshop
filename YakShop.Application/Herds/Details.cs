using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YakShop.Domain;
using YakShop.Persistent;

namespace YakShop.Application.Herd
{
    public class Details
    {
        public class Query : IRequest<List<Yak>> {
            public int Days { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Yak>>
        {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<Yak>> Handle(Query request, CancellationToken cancellationToken)
            {
                var yaks = await _context.Yaks.ToListAsync();

                foreach (var item in yaks)
                {
                    item.IncreaseAgeByDays(request.Days);
                }

                return yaks;
            }
        }
    }
}
