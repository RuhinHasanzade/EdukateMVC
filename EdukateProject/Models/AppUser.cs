using Microsoft.AspNetCore.Identity;

namespace EdukateProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
