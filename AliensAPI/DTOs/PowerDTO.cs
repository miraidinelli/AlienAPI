namespace AliensAPI.DTOs
{
	public class PowerDTO
	{
		public string Type { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public List<string> Aliens { get; set; } = new List<string>();
	}
}
