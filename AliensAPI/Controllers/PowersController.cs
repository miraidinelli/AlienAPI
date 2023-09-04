using Microsoft.AspNetCore.Mvc;
using AliensAPI.Models;
using AliensAPI.DTOs;
using AliensAPI.Services.Power;
using System.Numerics;

namespace AliensAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PowersController : ControllerBase
	{
		private readonly IPowerService _service;
		public PowersController(IPowerService powerService) { _service = powerService; }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<PowerModel>>> GetAllAsync()
		{
			var powers = await _service.GetAllAsync();

			if (powers is null) return BadRequest("An unexpected error occurred.");
			else return Ok(powers);
		}
		[HttpGet("types/{type}")]
		public async Task<ActionResult<IEnumerable<PowerModel>>> GetAllByTypeAsync(string type)
		{
			var powers = await _service.GetAllByTypeAsync(type);

			if (powers is null) return NotFound($"Entry \"{type}\" not found or inexistent");
			else return Ok(powers);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<PowerModel>> GetByIdAsync(int id)
		{
			var power = await _service.GetByIdAsync(id);

			if (power == null) return NotFound($"Entry \"{id}\" not found or inexistent.");
			else return Ok(power);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateByIdAsync(int id, PostPowerDTO postPowerDTO)
		{
			var power = await _service.UpdateByIdAsync(id, postPowerDTO);

			if (power is null) return NotFound($"Entry \"{id}\" not found or inexistent.");
            else return Ok($"Entry \"{id}\" updated successfully");
        }

		[HttpPost]
		public async Task<ActionResult> AddAsync(PostPowerDTO newPower)
		{
			var power = await _service.AddAsync(newPower);

            if (power == null)
                return BadRequest("Invalid data to create a new power.");
            else return Ok("Power successfully created!");
        }

		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
			var power = await _service.DeleteByIdAsync(id);

			if (power is null) return NotFound($"Entry \"{id}\" not found or inexistent.");
			return Ok($"Entry \"{id}\" successfully removed.");
		}
	}
}
