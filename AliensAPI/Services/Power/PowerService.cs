using AliensAPI.Models;
using Microsoft.EntityFrameworkCore;
using AliensAPI.Data;
using AliensAPI.DTOs;
using AliensAPI.Mappers;

namespace AliensAPI.Services.Power
{
	public class PowerService : IPowerService
	{
		private readonly DataContext _context;
		public PowerService(DataContext context) { _context = context; }

		public async Task<List<PowerDTO>> GetAllAsync()
		{
			var powers = await _context.Powers.Include(pow => pow.Aliens)
				.Select(pow => PowerMapper.ModelToDTO(pow)).ToListAsync();

			if (!powers.Any()) return null;
			else return powers;
		}
		public async Task<List<PowerDTO>> GetAllByTypeAsync(string type)
		{
			var powers = await _context.Powers.Include(power => power.Aliens)
				.Where(pwr => pwr.Type == type)
				.Select(pwr => PowerMapper.ModelToDTO(pwr)).ToListAsync();

			if (!powers.Any()) return null;
			else return powers;
		}
		public async Task<PowerDTO> GetByIdAsync(int id)
		{
			var power = await _context.Powers.Include(pow => pow.Aliens).FirstOrDefaultAsync(pow => pow.Id == id);
			var powerDTO =  PowerMapper.ModelToDTO(power);

			if (power is null) return null;
			else return powerDTO;
		}

		public async Task<int?> UpdateByIdAsync(int id, PostPowerDTO postPowerDTO)
		{
            var dbPowers = await _context.Powers.FindAsync(id);

            if (dbPowers is null) return null;
            else
            {
                PowerMapper powerMapper = new PowerMapper(_context);
                dbPowers = powerMapper.PutMapToEntity(postPowerDTO, dbPowers, _context);

                await _context.SaveChangesAsync();

                return 0;
            }
		}

        public async Task<int?> AddAsync(PostPowerDTO newPower)
        {
            PowerMapper powerMapper = new PowerMapper(_context);
            var power = powerMapper.PostMapToEntity(newPower);

            await _context.AddAsync(power);
            await _context.SaveChangesAsync();

            return 0;
        }

        public async Task<bool?> DeleteByIdAsync(int id)
        {
            var power = await _context.Powers.FindAsync(id);

            if (power is null) return null;
            else
            {
                _context.Powers.Remove(power);
                await _context.SaveChangesAsync();

                return true;
            }
        }
    }
}
