using Application.Abstraction;
using Contract.User.Request;
using Contract.User.Response;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserResponse GetUserById (int id) 
        { 
            var user = _userRepository.GetUserById(id);

            return new UserResponse
            {
                // Map domain User.Name -> DTO Name
                Name = user.Name,
                Email = user.Email
            };
        }

        public List<UserResponse> GetAllClients()
        {
            var users = _userRepository.GetAllClients();

            return users.Select(client => new UserResponse
            {
                Name = client.Name,
                Email = client.Email
            }).ToList();
        }

        public bool DeleteClient(int id)
        {
            return _userRepository.DeleteClient(id);
        }

        public bool CreateUser(CreateUserRequest request)
        {
            // Aca iria la logica de negocio ej: Si el email existe

            // Esto es el mapeo del DTO a entidad dominio
            var UserEntity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            }   
           ;
            return _userRepository.CreateUser(UserEntity);
        }

        public bool UpdateUser(int id, UpdateUserRequest request)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return false; // Usuario no encontrado
            }
            // Actualizar los campos del usuario con los datos del request
            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            return _userRepository.UpdateUser(user);

        }
    }
}
