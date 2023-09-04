using AliensAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AliensAPI.DTOs;
using AliensAPI.Services.Alien;

namespace AliensAPI.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class AliensController : ControllerBase
	{
		private readonly IAlienService _service;
		public AliensController(IAlienService service) { _service = service; }

		[HttpGet]
		public async Task<ActionResult<List<AlienModel>>> GetAllAsync()
		{
			var aliens = await _service.GetAllAsync();

			if (aliens is null)
			{
				return BadRequest("An unexpected error occurred.");
			}
			return Ok(aliens);
		}
		[HttpGet("planets/native/{native}")]
		public async Task<ActionResult<List<AlienDTO>>> GetAllByNativePlanetAsync(string native)
		{
			var aliens = await _service.GetAllByNativePlanetAsync(native);

			if (aliens is null)
			{
				return NotFound($"Entry \"{native}\" not found or inexistent.");
			}
			return Ok(aliens);
		}
		[HttpGet("planets/current/{current}")]
		public async Task<ActionResult<List<AlienDTO>>> GetAllByCurrentPlanetAsync(string current)
		{
			var aliens = await _service.GetAllByCurrentPlanetAsync(current);

			if (aliens is null)
			{
				return NotFound($"Entry \"{current}\" not found or inexistent.");
			}
			return Ok(aliens);
		}
		[HttpGet("powers/{power}")]
		public async Task<ActionResult<List<AlienDTO>>> GetAliensWithPowerAsync(string power)
		{
			var aliens = await _service.GetAliensWithPowerAsync(power);

			if (aliens is null) return NotFound($"Entry \"{power}\" not found or inexistent.");
			else return Ok(aliens);
		}
		[HttpGet("iids/{iId}")]
		public async Task<ActionResult<AlienDTO>> GetByIIdAsync(string iId)
		{
			var alien = await _service.GetByIIdAsync(iId);

			if (alien is null) return NotFound($"Entry \"{iId}\" not found or inexistent.");
			else return Ok(alien);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<AlienModel>> GetByIdAsync(int id)
		{
			var alien = await _service.GetByIdAsync(id);

			if (alien is null) return NotFound($"Entry \"{id}\" not found or inexistent.");
			else return Ok(alien);
		}

		[HttpPut("iids/{iids}")]
		public async Task<IActionResult> UpdateByIIdAsync(string iids, AlienDTO alienDTO)
		{
			var alien = await _service.UpdateByIIdAsync(iids, alienDTO);

			if (alien is null) return NotFound($"Entry \"{iids}\" not found or inexistent.");
			else if (alien is 1) return NotFound($"Entry \"{alienDTO.NativePlanet}\" not found or inexistent.");
			else if (alien is 2) return NotFound($"Entry in \"{alienDTO.Powers.ToList()}\" not found or inexistent.");
			else if (alien is 3) return NotFound($"Entry \"{alienDTO.OriginPlanet}\" not found or inexistent.");
			else if (alien is 4) return NotFound($"Entry \"{alienDTO.CurrentPlanet}\" not found or inexistent.");
			else return Ok($"Entry \"{iids} updated successfully.");
		}

		[HttpPost]
		public async Task<IActionResult> AddAsync(PostAlienDTO newAlien)
		{
			var alien = await _service.AddAsync(newAlien);

			if (alien is 1) return NotFound($"Entry native planet \"{newAlien.NativePlanet}\" not found or inexistent.");
			else if (alien is 2) return NotFound($"Entry in powers \"{newAlien.Powers.ToList()}\" not found or inexistent.");
			else if (alien is 3) return NotFound($"Entry in origin planets\"{newAlien.OriginPlanet}\" not found or inexistent.");
			else if (alien is 4) return NotFound($"Entry current planets\"{newAlien.CurrentPlanet}\" not found or inexistent.");
			else return Ok("");
			
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteByIdAsync(int id)
		{
			var alien = await _service.DeleteByIdAsync(id);

			if (alien is null) return NotFound($"Entry \"{id}\" not found or inexistent.");
			else return Ok($"Entry \"{id}\" successfully removed.");
		}
	}
}
