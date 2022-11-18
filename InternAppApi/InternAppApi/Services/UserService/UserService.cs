using InternAppApi.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace InternAppApi.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(DataContext context, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<IdentityResult>> DeleteUser(string id)
        {
            var response = new ServiceResponse<IdentityResult>();
            IdentityUser user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                response.Data = result;
            }
            else
            {
                response.Success = false;
                response.Message = "User Not Found.";
            }

            return response;

        }

        public async Task<ServiceResponse<IList<IdentityUser>>> GetUsers()
        {
            var response = new ServiceResponse<IList<IdentityUser>>();

            response.Data = _userManager.GetUsersInRoleAsync("Editor")
                .Result;

            return response;
        }



    }
}