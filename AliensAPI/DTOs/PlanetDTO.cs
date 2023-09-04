namespace AliensAPI.DTOs
{
	public class PlanetDTO
	{
		public string Name { get; set; } = string.Empty;
		public string NativeSpecies { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<string> CurrentNativePopulation { get; set; } = new List<string>();
		public List<string> CurrentImmigrantPopulation { get; set; } = new List<string>();
		public List<string> CurrentTourists { get; set; } = new List<string>();
	}
}
