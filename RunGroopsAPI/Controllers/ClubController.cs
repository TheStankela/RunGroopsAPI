using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace RunGroopsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClubsAsync()
        {
            var result = await _clubRepository.GetClubsAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClubByIdAsync(int id)
        {
            var result = await _clubRepository.GetClubByIdAsync(id);
            return result is not null ? Ok(result) : NotFound();
        }
        [HttpGet("city/{cityName}")]
        public async Task<IActionResult> GetClubsByCity(string cityName)
        {
            var result = await _clubRepository.GetClubsByCityAsync(cityName);
            return Ok(result);
        }
        [HttpGet("name={clubName}")]
        public async Task<IActionResult> GetClubByName(string clubName)
        {
            var result = await _clubRepository.GetClubByNameAsync(clubName);
            return result is not null ? Ok(result) : NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> AddClub(Club club)
        {
            if (!await _clubRepository.AddClubAsync(club)) return BadRequest();

            return Ok();
        }
    }
}
