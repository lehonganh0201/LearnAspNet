using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LearnPageRazor.Areas.Admin.Pages.User
{
    public class AddRole : UserPageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public AddRole(UserManager<Models.User> userManager, MyBlogContext context, RoleManager<IdentityRole> roleManager) : base(userManager, context)
        {
            _roleManager = roleManager;
        }
        
        public Models.User User { get; set; }
        
        [BindProperty]
        [DisplayName("Các vai trò của người dùng")]
        public List<string> Roles { get; set; }
        
        public SelectList RoleNames { get; set; }

        public async Task<IActionResult> OnGet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("Not Found User");
            }

            User = await _userManager.FindByIdAsync(userId);
            if (User == null)
            {
                return NotFound("Not Found User");
            }

            Roles = new List<string>(await _userManager.GetRolesAsync(User));

            RoleNames = new SelectList(await _roleManager.Roles.Select(r => r.Name).ToListAsync());

            return Page();
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("Not Found User");
            }

            User = await _userManager.FindByIdAsync(userId);
            if (User == null)
            {
                return NotFound("Not Found User");
            }

            var originalRoleNames = await _userManager.GetRolesAsync(User);

            var deleteRoles = originalRoleNames.Where(r => !Roles.Contains(r)).ToList();
            var addRoles = Roles.Where(r => !originalRoleNames.Contains(r)).ToList();

            var result = await _userManager.RemoveFromRolesAsync(User, deleteRoles);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }

            var resultAddRole = await _userManager.AddToRolesAsync(User, addRoles);
            if (!resultAddRole.Succeeded)
            {
                resultAddRole.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
                return Page();
            }

            StatusMessage = $"Bạn vừa cập nhật vai trò cho người dùng: {User.UserName}";
            return RedirectToPage("./Index");
        }
    }
}
