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
    public class Create : RolePageModel
    {
        public Create(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
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

        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var newRole = new IdentityRole(Input.Name);
            var result  = await _roleManager.CreateAsync(newRole);
            
            if (result.Succeeded)
            {
                StatusMessage = $"Vai trò {Input.Name} vừa được tạo thành công";
                return RedirectToPage("./Index");
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