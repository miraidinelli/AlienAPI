using AliensAPI.DTOs;
using AliensAPI.Models;

namespace AliensAPI.Services.Planet
{
	public interface IPlanetService
	{
		public Task<List<PlanetDTO>> GetAllAsync();
		public Task<PlanetDTO> GetByNativeSpeciesAsync(string species);
		public Task<PlanetDTO> GetByIdAsync(int id);

		public Task<int?> UpdateByIdAsync(int id, PostPlanetDTO postPlanetDTO);

		public Task<int?> AddAsync(PostPlanetDTO newPlanet);
        public Task<bool?> DeleteByIdAsync(int id);
    }
}
