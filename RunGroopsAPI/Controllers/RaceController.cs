using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Domain.Interfaces;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRacesAsync([FromQuery]int page)
        {
            return Ok();
        }
        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetAllUserRacesAsync()
        {
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRaceByIdAsync(int id)
        {
            return Ok();
        }
        [HttpGet("city={cityName}")]
        public async Task<IActionResult> GetRacesByCityAsync(string cityName)
        {
            return Ok();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRaceAsync()
        {
            return Ok();
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateRaceAsync([FromQuery]int raceId)
        {
            return Ok();
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteRaceAsync([FromQuery] int clubId)
        {
            return Ok();
        }
    }
}
