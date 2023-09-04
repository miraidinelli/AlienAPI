using Microsoft.EntityFrameworkCore;
using AliensAPI.Data;
using AliensAPI.Models;
using AliensAPI.DTOs;
using AliensAPI.Mappers;

namespace AliensAPI.Services.Planet
{
	public class PlanetService : IPlanetService
	{
		private readonly DataContext _context;
		public PlanetService(DataContext context) { _context = context; }

		public async Task<List<PlanetDTO>> GetAllAsync()
		{
			var planets = await _context.Planets.Include(plt => plt.CurrentNativePopulation)
				.Select(plt => PlanetMapper.ModelToDTO(plt)).ToListAsync();

			if (!planets.Any()) return null;
			else return planets;
		}
		public async Task<PlanetDTO> GetByNativeSpeciesAsync(string species)
		{
			var planet = await _context.Planets.Include(plt => plt.CurrentNativePopulation)
                .Where(plt => plt.NativeSpecies.ToLower() == species.ToLower())
				.Select(plt => PlanetMapper.ModelToDTO(plt)).FirstOrDefaultAsync();

			if (planet is null) return null;
			else return planet;
		}
		public async Task<PlanetDTO> GetByIdAsync(int id)
		{
			var planet = await _context.Planets.Include(pln => pln.CurrentImmigrantPopulation).FirstOrDefaultAsync(p => p.Id == id);

			if (planet is null) return null;
			else return PlanetMapper.ModelToDTO(planet);
		}

		public async Task<int?> UpdateByIdAsync(int id, PostPlanetDTO postPlanetDTO)
		{
			var dbPlanet = await _context.Planets.FindAsync(id);

			if (dbPlanet is null) return null;
			else
			{
				PlanetMapper planetMapper = new PlanetMapper(_context);
				dbPlanet = planetMapper.PutMapToEntity(postPlanetDTO, dbPlanet, _context);

				await _context.SaveChangesAsync();

				return 0;
			}
		}

		public async Task<int?> AddAsync(PostPlanetDTO newPlanet)
		{
			PlanetMapper planetMapper = new PlanetMapper(_context);
			var planet = planetMapper.PostMapToEntity(newPlanet);    

                await _context.AddAsync(planet);
                await _context.SaveChangesAsync();

                return 0;
        }

        public async Task<bool?> DeleteByIdAsync(int id)
        {
            var planet = await _context.Planets.FindAsync(id);

            if (planet is null) return null;
            else
            {
                _context.Planets.Remove(planet);
                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
