using AliensAPI.Models;

namespace AliensAPI.DTOs
{
    public class PostAlienDTO
    {
    
        public string IId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public int Age { get; set; } = new int();
        public double Height { get; set; } = new double();
        public double Weight { get; set; } = new double();
        public string Description { get; set; } = string.Empty;
        public string NativePlanet { get; set; } = string.Empty;  
        public List<string> Powers { get; set; } = new List<string>();
        public string OriginPlanet { get; set; } = string.Empty;       
        public DateTime OriginArrival { get; set; } = DateTime.MinValue;
        public DateTime OriginDeparture { get; set; } = DateTime.MinValue;
        public string CurrentPlanet { get; set; } =  string.Empty;      
        public DateTime CurrentArrival { get; set; } = DateTime.MinValue;
        public DateTime CurrentDeparture { get; set; } = DateTime.MinValue;
    }
}
