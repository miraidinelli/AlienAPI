namespace AliensAPI.Models
{
	public class PowerModel
	{
		public int Id { get; set; } = new int();
		public string Type { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;

		public List<AlienModel> Aliens { get; set; } = new List<AlienModel>();
	}
}
