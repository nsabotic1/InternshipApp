using Microsoft.AspNetCore.Identity;

namespace InternAppApi.Models
{
    public class User : IdentityUser
    {
        public string? Email { get; set; }
    }
}