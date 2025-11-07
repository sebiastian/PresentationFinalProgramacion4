namespace Contract.Location.Response;

public class LocationResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SpaceType { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public string WeatherDescription { get; set; } // Ej: "Despejado", "Lluvia"
    public double Temperature { get; set; } // Ej: 22.5
}