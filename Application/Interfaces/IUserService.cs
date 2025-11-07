// Archivo: Application/Service/IUserService.cs
// Propósito: Define el contrato (interfaz) que el servicio de usuario debe implementar.
// Esta interfaz permite desacoplar la capa de presentación (controladores) de la
// implementación concreta del servicio, facilitando pruebas unitarias y sustitución.

using Contract.User.Request;
using Contract.User.Response; // Importa el DTO que se usará en el método de la interfaz

namespace Application.Interfaces
{
    // Interfaz pública que declara las operaciones que el servicio de usuario ofrece.
    // No contiene lógica, solo la firma de los métodos (contrato).
    public interface IUserService
    {
        UserResponse GetUserById(int id);
        List<UserResponse> GetAllClients();

        bool DeleteClient(int id);

        bool CreateUser(CreateUserRequest request);

        bool UpdateUser(int id, UpdateUserRequest request);


    }

}