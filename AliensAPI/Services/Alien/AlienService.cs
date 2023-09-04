using AliensAPI.Models;
using Microsoft.EntityFrameworkCore;
using AliensAPI.Data;
using AliensAPI.DTOs;
using AliensAPI.Mappers;

namespace AliensAPI.Services.Alien
{
	public class AlienService : IAlienService
	{
		private readonly DataContext _context;
		public AlienService(DataContext context) { _context = context; }

		public async Task<List<AlienDTO>> GetAllAsync()
		{
			var aliens = await _context.Aliens.Include(plt => plt.NativePlanet).Include(pow => pow.Powers)
				.Select(aln => AlienMapper.ModelToDTO(aln)).ToListAsync();

			if (!aliens.Any()) return null;
			else return aliens;
		}
		public async Task<List<AlienDTO>> GetAllByNativePlanetAsync(string planet)
		{
			var aliens = await _context.Aliens.Include(plt => plt.NativePlanet).Include(pow => pow.Powers)
				.Where(aln => aln.OriginPlanet.Name.ToLower() == planet.ToLower())
				.Select(aln => AlienMapper.ModelToDTO(aln)).ToListAsync();

			if (!aliens.Any()) return null;
			else return aliens;
		}
		public async Task<List<AlienDTO>> GetAllByCurrentPlanetAsync(string planet)
		{
			var aliens = await _context.Aliens.Include(plt => plt.CurrentPlanet).Include(pow => pow.Powers)
                .Where(aln => aln.CurrentPlanet.Name.ToLower() == planet.ToLower())
				.Select(aln => AlienMapper.ModelToDTO(aln)).ToListAsync();

			if (!aliens.Any()) return null;
			else return aliens;
		}
		public async Task<List<AlienDTO>> GetAliensWithPowerAsync(string power)
		{
			var aliens = await _context.Aliens.Include(plt => plt.NativePlanet).Include(pow => pow.Powers)
				.Where(aln => aln.Powers.Any(pwr => pwr.Name.ToLower() == power.ToLower()))
				.Select(aln => AlienMapper.ModelToDTO(aln)).ToListAsync();

			if (!aliens.Any()) return null;
			else return aliens;
		}
		public async Task<AlienDTO> GetByIIdAsync(string iId)
		{
			var alien = await _context.Aliens.Include(pow => pow.Powers).Include(plt => plt.CurrentPlanet).FirstOrDefaultAsync(aln => aln.IId.ToLower() == iId.ToLower());
            var alienDto = AlienMapper.ModelToDTO(alien);

            if (alien is null) return null;
			else return alienDto;
		}
		public async Task<AlienDTO> GetByIdAsync(int id)
		{
			var alien = await _context.Aliens.Include(plt => plt.NativePlanet).Include(pow => pow.Powers).FirstOrDefaultAsync(a => a.Id == id);
			var alienDto = AlienMapper.ModelToDTO(alien);

			if (alien is null) return null;
			else return alienDto;
		}

		public async Task<int?> UpdateByIIdAsync(string iId, AlienDTO alienDTO)
		{
			var dbAlien = await _context.Aliens.FirstOrDefaultAsync(aln => aln.IId == iId);

			if (dbAlien is null) return null;
			else
			{
				AlienMapper alienMapper = new AlienMapper(_context);
				dbAlien = alienMapper.PutMapToEntity(alienDTO, dbAlien);

				var dbNativePlanet = await _context.Planets.FirstOrDefaultAsync(plt => plt.Name.ToLower() == alienDTO.NativePlanet.ToLower());
				var dbOriginPlanet = await _context.Planets.FirstOrDefaultAsync(plt => plt.Name.ToLower() == alienDTO.OriginPlanet.ToLower());
				var dbCurrentPlanet = await _context.Planets.FirstOrDefaultAsync(plt => plt.Name.ToLower() == alienDTO.CurrentPlanet.ToLower());

				if (dbNativePlanet is null) return 1;
				else dbAlien.NativePlanet.Name = alienDTO.NativePlanet;

				dbAlien.Powers.Clear();
				foreach (string power in alienDTO.Powers)
				{
					var dbPower = await _context.Powers.FirstOrDefaultAsync(pwr => pwr.Name.ToLower() == power.ToLower());

					if (dbPower is null) return 2;
					else dbAlien.Powers.Add(dbPower);
				}

				if (dbOriginPlanet is null) return 3;
				else dbAlien.OriginPlanet.Name = alienDTO.OriginPlanet;

				if (dbCurrentPlanet is null) return 4;
				else dbAlien.CurrentPlanet.Name = alienDTO.CurrentPlanet;
				await _context.SaveChangesAsync();

				return 0;
			}
		}

		public async Task<int?> AddAsync(PostAlienDTO newAlien)
		{
			AlienMapper alienMapper = new AlienMapper(_context);
			var alien = alienMapper.PostMapToEntity(newAlien);

		

			var dbNativePlanet = await _context.Planets.FirstOrDefaultAsync(plt => plt.Name.ToLower() == alien.NativePlanet.Name.ToLower());
			var dbOriginPlanet = await _context.Planets.FirstOrDefaultAsync(plt => plt.Name.ToLower() == alien.OriginPlanet.Name.ToLower());
			var dbCurrentPlanet = await _context.Planets.FirstOrDefaultAsync(plt => plt.Name.ToLower() == alien.CurrentPlanet.Name.ToLower());

			if (dbNativePlanet is null) return 1;
			else
			{
				newAlien.NativePlanet = dbNativePlanet.Name;

				newAlien.Powers.Clear();

                foreach (var power in newAlien.Powers)
                {
                    var dbPower = await _context.Powers.FirstOrDefaultAsync(pwr => pwr.Name.ToLower() == power.ToLower());

                    if (dbPower is null) return 2;
                    else alien.Powers.Add(dbPower);
                }

                if (dbOriginPlanet is null) return 3;
                else alien.OriginPlanet = dbOriginPlanet;

                if (dbCurrentPlanet is null) return 4;
                else alien.CurrentPlanet = dbCurrentPlanet;

                await _context.AddAsync(alien);
                await _context.SaveChangesAsync();

                return 0;
            }
			

		}
		public async Task<bool?> DeleteByIdAsync(int id)
		{
			var alien = await _context.Aliens.FindAsync(id);

			if (alien is null) return null;
			else
			{
				_context.Aliens.Remove(alien);
				await _context.SaveChangesAsync();

				return true;
			}
		}
	}
}
