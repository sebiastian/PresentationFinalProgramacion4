namespace Domain.Entity;

public class Photographer : User
{
    // Hereda Id, Nombre, Email, Telefono desde User
    public string PasswordHash { get; set; }
    public string Rol { get; set; }
}
