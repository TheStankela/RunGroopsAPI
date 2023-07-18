﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Application.Commands.RaceCommands;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.Interfaces;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RaceController(IRaceRepository raceRepository, IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetRacesAsync([FromQuery]int page)
        {
            var query = new GetRacesQuery(page);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetAllUserRacesAsync()
        {
            var query = new GetAllUserRacesQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRaceByIdAsync(int id)
        {
            var query = new GetRaceByIdQuery(id);
            var result = await _mediator.Send(query);

            return result is not null ? Ok(result) : NotFound();
        }
        [HttpGet("name={raceName}")]
        public async Task<IActionResult> GetRacesByNameAsync(string raceName)
        {
            var query = new GetRacesByNameQuery(raceName);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [HttpGet("city/{cityName}")]
        public async Task<IActionResult> GetRacesByCityAsync(string cityName)
        {
            var query = new GetRacesByCityQuery(cityName);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRaceAsync([FromForm]RaceRequest request, IFormFile file)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddRaceCommand(request, file);
            var result = await _mediator.Send(command);

            return result is true ? Ok(new { Message = "Created Successfully!"}) : BadRequest(ModelState);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateRaceAsync([FromQuery]int raceId, RaceRequest raceRequest)
        {
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteRaceAsync([FromQuery] int raceId)
        {
            return Ok();
        }
    }
}
