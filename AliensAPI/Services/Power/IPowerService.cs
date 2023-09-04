using AliensAPI.DTOs;
using AliensAPI.Models;

namespace AliensAPI.Services.Power
{
	public interface IPowerService
	{
		public Task<List<PowerDTO>> GetAllAsync();
		public Task<List<PowerDTO>> GetAllByTypeAsync(string type);
		public Task<PowerDTO> GetByIdAsync(int id);
		public Task<int?> UpdateByIdAsync(int id, PostPowerDTO postPowerDTO);
        public Task<int?> AddAsync(PostPowerDTO newPower);
        public Task<bool?> DeleteByIdAsync(int id);
	}
}
