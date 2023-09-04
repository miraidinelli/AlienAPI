using AliensAPI.Data;
using AliensAPI.DTOs;
using AliensAPI.Models;

namespace AliensAPI.Mappers
{
    public class AlienMapper
    {
        private readonly DataContext _context;
        public AlienMapper(DataContext context) { _context = context; }

        public static AlienDTO ModelToDTO(AlienModel alienModel)
        {
            AlienDTO alienDTO = new AlienDTO
            {
                IId = alienModel.IId,
                Name = alienModel.Name,
                Species = alienModel.Species,
                Age = alienModel.Age,
                Height = alienModel.Height,
                Weight = alienModel.Weight,
                Description = alienModel.Description,
                NativePlanet = alienModel.NativePlanet.Name,

                Powers = alienModel.Powers.Select(pwr => pwr.Name).ToList(),

                OriginPlanet = alienModel.OriginPlanet.Name,
                OriginArrival = alienModel.OriginArrival,
                OriginDeparture = alienModel.OriginDeparture,

                CurrentPlanet = alienModel.CurrentPlanet.Name,
                CurrentArrival = alienModel.CurrentArrival,
                CurrentDeparture = alienModel.CurrentDeparture
            };

            return alienDTO;
        }

        public static AlienModel DTOToModel(AlienDTO alienDTO, DataContext context)
        {
            var dbNativePlanet = context.Planets.FirstOrDefault(pln => pln.Name == alienDTO.NativePlanet);
            var dbPowers = context.Powers.Where(pwr => alienDTO.Powers.Contains(pwr.Name)).ToList();
            var dbOriginPlanet = context.Planets.FirstOrDefault(pln => pln.Name == alienDTO.OriginPlanet);
            var dbCurrentPlanet = context.Planets.FirstOrDefault(pln => pln.Name == alienDTO.CurrentPlanet);

            AlienModel alienModel = new AlienModel
            {
                IId = alienDTO.IId,
                Name = alienDTO.Name,
                Species = alienDTO.Species,
                Age = alienDTO.Age,
                Height = alienDTO.Height,
                Weight = alienDTO.Weight,
                Description = alienDTO.Description,
                NativePlanet = dbNativePlanet,

                Powers = dbPowers,

                OriginPlanet = dbOriginPlanet,
                OriginArrival = alienDTO.OriginArrival,
                OriginDeparture = alienDTO.OriginDeparture,

                CurrentPlanet = dbCurrentPlanet,
                CurrentArrival = alienDTO.CurrentArrival,
                CurrentDeparture = alienDTO.CurrentDeparture
            };

            return alienModel;
        }

        public AlienModel PutMapToEntity(AlienDTO alienDTO, AlienModel alienModel)
        {
            alienModel.IId = alienDTO.IId;
            alienModel.Name = alienDTO.Name;
            alienModel.Species = alienDTO.Species;
            alienModel.Age = alienDTO.Age;
            alienModel.Height = alienDTO.Height;
            alienModel.Weight = alienDTO.Weight;
            alienModel.Description = alienDTO.Description;

            alienModel.OriginArrival = alienDTO.OriginArrival;
            alienModel.OriginDeparture = alienDTO.OriginDeparture;

            alienModel.CurrentArrival = alienDTO.CurrentArrival;
            alienModel.CurrentDeparture = alienDTO.CurrentDeparture;

            return alienModel;
        }

        public AlienModel PostMapToEntity(PostAlienDTO newAlien)
        {
            AlienModel alienModel = new AlienModel
            {
                IId = newAlien.IId,
                Name = newAlien.Name,
                Species = newAlien.Species,
                Age = newAlien.Age,
                Height = newAlien.Height,
                Weight = newAlien.Weight,
                NativePlanet = _context.Planets.FirstOrDefault(plt => plt.Name.ToLower() == newAlien.NativePlanet.ToLower()),
                CurrentPlanet = _context.Planets.FirstOrDefault(plt => plt.Name.ToLower() == newAlien.CurrentPlanet.ToLower()),
                OriginPlanet = _context.Planets.FirstOrDefault(plt => plt.Name.ToLower() == newAlien.OriginPlanet.ToLower()),
                Description = newAlien.Description,
                OriginArrival = newAlien.OriginArrival,
                OriginDeparture = newAlien.OriginDeparture,
                CurrentArrival = newAlien.CurrentArrival,
                CurrentDeparture = newAlien.CurrentDeparture
            };

            return alienModel;
        }
    }
}

