using AliensAPI.Data;
using AliensAPI.DTOs;
using AliensAPI.Models;

namespace AliensAPI.Mappers
{
	public class PlanetMapper
	{
		private readonly DataContext _dataContext;
		public PlanetMapper(DataContext dataContext) { _dataContext = dataContext; }

		public static PlanetDTO ModelToDTO(PlanetModel planetModel)
		{

			PlanetDTO planetDTO = new PlanetDTO()
			{
				Name = planetModel.Name,
				NativeSpecies = planetModel.NativeSpecies,
				Description = planetModel.Description,
				CurrentNativePopulation = planetModel.CurrentNativePopulation.Select(aln => aln.IId).ToList(),
				CurrentImmigrantPopulation = planetModel.CurrentImmigrantPopulation.Select(aln => aln.IId).ToList(),

				CurrentTourists = planetModel.CurrentTourists.Select(aln => aln.IId).ToList()
			};

			return planetDTO;
		}

		public PlanetModel PutMapToEntity(PostPlanetDTO postPlanetDTO, PlanetModel planetModel, DataContext context)
		{
			planetModel.Name = postPlanetDTO.Name;
			planetModel.NativeSpecies = postPlanetDTO.NativeSpecies;
			planetModel.Description = postPlanetDTO.Description;
			
			/*planetModel.CurrentNativePopulation = planetDTO.CurrentNativePopulation.Where(alnDTO => planetModel.CurrentNativePopulation.Any(alnModel => alnModel.IId.ToLower() == alnDTO.ToLower()))
				.Select(alnDTO => AlienMapper.DTOToModel(AlienMapper.ModelToDTO(context.Aliens.FirstOrDefault(al => al.IId == alnDTO)), context)).ToList();

			planetModel.CurrentImmigrantPopulation = planetDTO.CurrentImmigrantPopulation.Where(alnDTO => planetModel.CurrentImmigrantPopulation.Any(alnModel => alnModel.IId.ToLower() == alnDTO.ToLower()))
				.Select(alnDTO => AlienMapper.DTOToModel(AlienMapper.ModelToDTO(context.Aliens.FirstOrDefault(al => al.IId == alnDTO)), context)).ToList();

			planetModel.CurrentTourists = planetDTO.CurrentTourists.Where(alnDTO => planetModel.CurrentTourists.Any(alnModel => alnModel.IId.ToLower() == alnDTO.ToLower()))
				.Select(alnDTO => AlienMapper.DTOToModel(AlienMapper.ModelToDTO(context.Aliens.FirstOrDefault(al => al.IId == alnDTO)), context)).ToList();*/

			return planetModel;
		}

		public PlanetModel PostMapToEntity(PostPlanetDTO newPlanet)
		{
			PlanetModel planetModel = new PlanetModel
			{
				Name = newPlanet.Name,
				NativeSpecies = newPlanet.NativeSpecies,
				Description = newPlanet.Description,
			};

			return planetModel;
		}
	}
}
