using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearnPageRazor.Areas.Admin.Role;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnPageRazor.Areas.Admin.Pages.Role
{
    public class Delete : RolePageModel
    {
        public Delete(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public IdentityRole Role { get; set; }

        public async Task<IActionResult> OnGet(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return NotFound("Không tìm thấy vai trò");
            }
            Role = await _roleManager.FindByIdAsync(roleId);
            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (roleid == null)
            {
                return NotFound("Không tìm thấy role");
            }
            Role = await _roleManager.FindByIdAsync(roleid);
            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            var result = await _roleManager.DeleteAsync(Role);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa xóa vai trò {Role.Name}";
                return RedirectToPage("Index");
            }
            else
            {
                result.Errors.ToList().ForEach(error =>
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }

            return Page();
        }
    }
}