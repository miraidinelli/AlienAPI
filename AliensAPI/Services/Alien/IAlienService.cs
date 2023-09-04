using AliensAPI.Models;
using AliensAPI.DTOs;

namespace AliensAPI.Services.Alien
{
	public interface IAlienService
	{
		public Task<List<AlienDTO>> GetAllAsync();
		public Task<List<AlienDTO>> GetAllByNativePlanetAsync(string planet);
		public Task<List<AlienDTO>> GetAllByCurrentPlanetAsync(string planet);
		public Task<List<AlienDTO>> GetAliensWithPowerAsync(string power);
		public Task<AlienDTO> GetByIIdAsync(string iId);
		public Task<AlienDTO> GetByIdAsync(int id);

		public Task<int?> UpdateByIIdAsync(string iId, AlienDTO alienDTO);

		public Task<int?> AddAsync(PostAlienDTO newAlien);
		public Task<bool?> DeleteByIdAsync(int id);
	}
}
