namespace AliensAPI.Models
{
	public class AlienModel
	{
		public int Id { get; set; } = new int();
		public string IId { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Species { get; set; } = string.Empty;
		public int Age { get; set; } = new int();
		public double Height { get; set; } = new double();
		public double Weight { get; set; } = new double();
		public string Description { get; set; } = string.Empty;
		public int NativePlanetId { get; set; } = new int();
		public PlanetModel NativePlanet { get; set; } = new PlanetModel();

		public List<PowerModel> Powers { get; set; } = new List<PowerModel>();

		public int OriginPlanetId { get; set; } = new int();
		public PlanetModel OriginPlanet { get; set; } = new PlanetModel();
		public DateTime OriginArrival { get; set; } = DateTime.MinValue;
		public DateTime OriginDeparture { get; set; } = DateTime.MinValue;

		public int CurrentPlanetId { get; set; } = new int();
		public PlanetModel CurrentPlanet { get; set; } = new PlanetModel();
		public DateTime CurrentArrival { get; set; } = DateTime.MinValue;
		public DateTime CurrentDeparture { get; set; } = DateTime.MinValue;
	}
}
