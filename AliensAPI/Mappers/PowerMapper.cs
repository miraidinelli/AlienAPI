using AliensAPI.Data;
using AliensAPI.DTOs;
using AliensAPI.Models;

namespace AliensAPI.Mappers
{
	public class PowerMapper
	{
        private readonly DataContext _dataContext;
        public PowerMapper(DataContext dataContext) { _dataContext = dataContext; }
        public static PowerDTO ModelToDTO(PowerModel powerModel)
		{
			PowerDTO powerDTO = new PowerDTO()
			{
				Type = powerModel.Type,
				Name = powerModel.Name,
				Description = powerModel.Description,

				Aliens = powerModel.Aliens.Select(aln => aln.Name).ToList()
			};

			return powerDTO;
		}

        public PowerModel PostMapToEntity(PostPowerDTO newPower)
        {
            PowerModel powerModel = new PowerModel
            {
                Type = newPower.Type,
                Name = newPower.Name,
                Description = newPower.Description,
            };

            return powerModel;
        }

        public PowerModel PutMapToEntity(PostPowerDTO postPowerDTO, PowerModel powerModel, DataContext context)
        {
            powerModel.Type = postPowerDTO.Type;
            powerModel.Name = postPowerDTO.Name;
            powerModel.Description = postPowerDTO.Description;         

            return powerModel;
        }
    }
}
