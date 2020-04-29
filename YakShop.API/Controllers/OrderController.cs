using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using YakShop.Application.Orders;
using YakShop.Domain;

namespace YakShop.API.Controllers
{
    [Route("yak-shop/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{days}")]
        public async Task<ActionResult<Unit>> Create(int days, Create.Command command)
        {
            if (days < 0) {
                return BadRequest("Days can not be negative");
            }
            command.Days = days;
            var successOrder = await _mediator.Send(command);
            if (successOrder.Milk == command.Order.Milk && successOrder.Skins == command.Order.Skins)
            {
                return StatusCode(StatusCodes.Status201Created, JsonConvert.SerializeObject(successOrder, Formatting.Indented));
            }
            if (successOrder.Milk > 0.0 || successOrder.Skins > 0)
            {
                return StatusCode(StatusCodes.Status206PartialContent, JsonConvert.SerializeObject(successOrder, Formatting.Indented));
            }

            return NotFound("");
        }

        [HttpGet]
        public async Task<ActionResult<List<Reservation>>> GetAllReservations()
        {
            var reservations = await _mediator.Send(new Details.Query());

            return Ok(JsonConvert.SerializeObject(reservations.OrderBy(x => x.Day), Formatting.Indented));
        }
    }
}