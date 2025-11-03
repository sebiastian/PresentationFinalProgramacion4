using System.Collections.Generic;

namespace Domain.Entity;

public class Location
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SpaceType { get; set; }
    public string Description { get; set; }

    // Navegación a sesiones
    public ICollection<PhotoSession> Sessions { get; set; } = new List<PhotoSession>();
}   