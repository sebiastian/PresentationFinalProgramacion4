namespace Domain.Entity;

public class Location
{
    public int Id { get; set; }
    // Name instead of Nombre
    public string Name { get; set; }
    // SpaceType instead of TipoEspacio
    public string SpaceType { get; set; }
    // Description instead of Descripcion
    public string Description { get; set; }

    // Navigation to PhotoSession
    public ICollection<PhotoSession> Sessions { get; set; } = new List<PhotoSession>();
}