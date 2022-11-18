using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InternAppApi.Auth;
using InternAppApi.Dtos.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using InternAppApi.Services.MailService;

namespace InternAppApi.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;

        public AuthService(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMailService mailService)
        {
            _userManager = userManager;
            
            _roleManager = roleManager;
            
            _configuration = configuration;
            
            _mailService = mailService;
        }
        
        public async Task<Boolean> UserExists(string username){
            var check = false;
            var userExists = await _userManager.FindByNameAsync(username);
            if(userExists != null) check = true; 
            return check;
        }

        public async Task<Boolean> AddUser(RegisterModel model) {
            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return false;
            }
        
            if (!await _roleManager.RoleExistsAsync(UserRoles.Editor))
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Editor));
            await _userManager.AddToRoleAsync(user, UserRoles.Editor);

            await _mailService.SendEmailAsync(user.Email, "Internship Dashboard", "There was an Editor account created with your email, your credentials are \nUsername:" +user.UserName+ "\nPassword:" + model.Password);

            return true;
        }
        

        public async Task<ServiceResponse<TokenDto>> DoLogin(LoginModel model){
            var response = new ServiceResponse<TokenDto>();
            response.Success = false;
        
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                var tokenData = new TokenDto{
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                };
                response.Data = tokenData;
                response.Success = true;
            }
            return response;
        }

         private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }


    }
}