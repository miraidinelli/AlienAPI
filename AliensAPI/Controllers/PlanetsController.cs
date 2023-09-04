using Microsoft.AspNetCore.Mvc;
using AliensAPI.Models;
using AliensAPI.Services.Planet;
using AliensAPI.DTOs;

namespace AliensAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlanetsController : ControllerBase
	{
		private readonly IPlanetService _service;
		public PlanetsController(IPlanetService planetService) { _service = planetService; }
		
		[HttpGet]
		public async Task<ActionResult<List<PlanetModel>>> GetAllAsync()
		{
			var planets = await _service.GetAllAsync();

			if (planets is null) return BadRequest("An unexpected error occurred.");
			else return Ok(planets);
		}
		[HttpGet("species/{species}")]
		public async Task<ActionResult<PlanetModel>> GetByNativeSpeciesAsync(string species)
		{
			var planet = await _service.GetByNativeSpeciesAsync(species);

			if (planet is null) return NotFound($"Entry \"{species}\" not found or inexistent.");
			else return Ok(planet);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<PlanetModel>> GetByIdAsync(int id)
		{
			var planet = await _service.GetByIdAsync(id);

			if (planet is null) return NotFound($"Entry \"{id}\" not found or inexistent.");
			else return Ok(planet);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateByIdAsync(int id, PostPlanetDTO postPlanetDTO)
		{
			var planet = await _service.UpdateByIdAsync(id, postPlanetDTO);

			if (planet is null) return NotFound($"Entry {id} not found or inexistent.");
			else return Ok($"Entry \"{id}\" updated successfully");
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync(PostPlanetDTO newPlanet)
		{
			var planet = await _service.AddAsync(newPlanet);

            if (planet == null)
                return BadRequest("Invalid data to create a new planet.");
            else return Ok("Planet successfully created!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var planet = await _service.DeleteByIdAsync(id);

            if (planet is null) return NotFound($"Entry \"{id}\" not found or inexistent.");
            return Ok($"Entry \"{id}\" successfully removed.");
        }
    }
}
