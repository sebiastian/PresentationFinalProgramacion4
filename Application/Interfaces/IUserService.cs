

using Contract.User.Request;
using Contract.User.Response; 

namespace Application.Interfaces
{
    
    public interface IUserService
    {
        UserResponse GetUserById(int id);
        List<UserResponse> GetAllClients();

        bool DeleteClient(int id);

        bool CreateUser(CreateUserRequest request);

        bool UpdateUser(int id, UpdateUserRequest request);


    }

}