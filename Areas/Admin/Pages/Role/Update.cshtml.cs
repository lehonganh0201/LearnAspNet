using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearnPageRazor.Areas.Admin.Role;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LearnPageRazor.Areas.Admin.Pages.Role
{
    [Authorize(Policy = "AllowEditRole")]
    public class Update : RolePageModel
    {
        public Update(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }
        
        public class InputModel
        {
            [DisplayName("Tên của vai trò")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} kí tự")]
            public string Name { get; set; }
        }
        
        [BindProperty]
        public InputModel Input { get; set; }
        
        public IdentityRole Role { get; set; }
        
        public List<IdentityRoleClaim<string>> Claims { get; set; } 


        public async Task<IActionResult> OnGet(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return NotFound("Không tìm thấy vai trò");
            }
            Role = await _roleManager.FindByIdAsync(roleId);
            if (Role != null)
            {
                Input = new InputModel()
                {
                    Name = Role.Name
                };
                Claims = await _context.RoleClaims.Where(r => r.RoleId == Role.Id).ToListAsync();

                return Page();
            }


            return NotFound("Không tìm thấy role");
        }

        public async Task<IActionResult> OnPost(string roleId)
        {
            if (roleId == null)
            {
                return NotFound("Không tìm thấy role");
            }

            Role = await _roleManager.FindByIdAsync(roleId);
            if (Role == null)
            {
                return NotFound("Không tìm thấy role");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Role.Name = Input.Name;
            var result = await _roleManager.UpdateAsync(Role);
            if (result.Succeeded)
            {
                StatusMessage = $"Bạn vừa đổi tên: {Input.Name}";
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