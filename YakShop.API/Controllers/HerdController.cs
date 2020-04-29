using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YakShop.API.Models;
using YakShop.Application;
using YakShop.Application.Herd;
using YakShop.Application.Herds;
using YakShop.Domain;

namespace YakShop.API.Controllers
{
    [Route("yak-shop/[controller]")]
    [ApiController]
    public class HerdController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HerdController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("{days}")]
        public async Task<ActionResult<HerdViewModel>> Details(int days)
        {
            if (days<0) {
                return BadRequest("Days can not be negative");
            }
            var yaks = await _mediator.Send(new Details.Query { Days = days });
            HerdViewModel hvm = new HerdViewModel(yaks, days);

            return hvm;
        }

        [HttpPut("upload")]
        public async Task<ActionResult> UpdateHerd([FromForm] IFormFile file)
        {
            //parse xml to yaks
            var xmlstring = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    xmlstring.AppendLine(reader.ReadLine());
            }

            Parser parser = new Parser();

            var newYaks = parser.Parse(xmlstring.ToString());

            await _mediator.Send(new Update.Command { Yaks = newYaks });
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}