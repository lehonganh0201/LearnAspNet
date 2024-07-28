using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LearnPageRazor.Areas.Admin.Role
{
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }
        
        public List<IdentityRole> Roles { get; set; }
        
        public async Task OnGet()
        {
            Roles = await _roleManager.Roles.OrderBy(role => role.Name).ToListAsync();
        }

        public void OnPost()
        {
            RedirectToPage();
        }
    }
}
