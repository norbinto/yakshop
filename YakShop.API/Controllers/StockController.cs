using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YakShop.Application.Stocks;
using YakShop.Domain;

namespace YakShop.API.Controllers
{
    [Route("yak-shop/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StockController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{days}")]
        public async Task<ActionResult<Stock>> Details(int days)
        {
            if (days < 0) {
                return BadRequest("Days can not be negative");
            }
            return await _mediator.Send(new Details.Query { Days = days });
        }
    }
}