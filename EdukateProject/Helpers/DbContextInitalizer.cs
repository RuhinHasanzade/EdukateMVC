using EdukateProject.Models;
using EdukateProject.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Identity;

namespace EdukateProject.Helpers
{
    public class DbContextInitalizer
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole > _roleManager;
        private readonly IConfiguration _configuration;
        private readonly AdminVm _adminVm;

        public DbContextInitalizer(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;

            _adminVm = _configuration.GetSection("AdminSettings").Get<AdminVm>() ?? new();
        }

        public async Task InitDatabase()
        {
            await CreateRoles();
            await CreateAdmin();
        }

        private async Task CreateAdmin()
        {
            AppUser admin = new()
            {
                FullName = _adminVm.FullName,
                UserName = _adminVm.Username,
                Email = _adminVm.Email
            };

            var result = await _userManager.CreateAsync(admin,_adminVm.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        private async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new() { Name = "Admin" });
            await _roleManager.CreateAsync(new() { Name = "Moderator" });
            await _roleManager.CreateAsync(new() { Name = "Member" });
        }
    }
}
