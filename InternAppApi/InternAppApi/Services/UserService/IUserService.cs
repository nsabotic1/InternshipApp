using Microsoft.AspNetCore.Identity;

namespace InternAppApi.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<IList<IdentityUser>>> GetUsers();

        Task<ServiceResponse<IdentityResult>> DeleteUser(string id);

    }
}