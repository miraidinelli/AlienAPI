namespace AliensAPI.Models
{
	public class PlanetModel
	{
		public int Id { get; set; } = new int();
		public string Name { get; set; } = string.Empty;
		public string NativeSpecies { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<AlienModel> CurrentNativePopulation { get; set; } = new List<AlienModel>();
		public List<AlienModel> CurrentImmigrantPopulation { get; set; } = new List<AlienModel>();

		public List<AlienModel> CurrentTourists { get; set; } = new List<AlienModel>();
	}
}
