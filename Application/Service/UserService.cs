using Domain.Abstraction;
using Application.Interfaces;
using Contract.User.Request;
using Contract.User.Response;
using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserResponse GetUserById(int id) 
        { 
            // REFACTORIZACIÓN: Cambio de .GetUserById() → .GetById()
            var user = _userRepository.GetById(id);

            return new UserResponse
            {
                Name = user.Name,
                Email = user.Email
            };
        }

        public List<UserResponse> GetAllClients()
        {
            // REFACTORIZACIÓN: Cambio de .GetAllClients() → .GetAll()
            var users = _userRepository.GetAll();

            return users.Select(client => new UserResponse
            {
                Name = client.Name,
                Email = client.Email
            }).ToList();
        }

        public bool DeleteClient(int id)
        {
            // REFACTORIZACIÓN: Cambio de .DeleteClient(id) → .Delete(id)
            return _userRepository.Delete(id);
        }

        public bool CreateUser(CreateUserRequest request)
        {
            // Aquí iría la lógica de negocio, ej: Validar si el email ya existe

            // Mapeo del DTO a entidad de dominio
            var userEntity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone
            };
            
            // REFACTORIZACIÓN: Cambio de .CreateUser(userEntity) → .Create(userEntity)
            return _userRepository.Create(userEntity);
        }

        public bool UpdateUser(int id, UpdateUserRequest request)
        {
            // REFACTORIZACIÓN: Cambio de .GetUserById(id) → .GetById(id)
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return false;
            }
            
            // Actualizar los campos del usuario con los datos del request
            user.Name = request.Name;
            user.Email = request.Email;
            user.Phone = request.Phone;
            
            // REFACTORIZACIÓN: Cambio de .UpdateUser(user) → .Update(user)
            return _userRepository.Update(user);
        }
    }
}
