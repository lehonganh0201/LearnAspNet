using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LearnPageRazor.Areas.Admin.Role
{
    [Authorize]
    public class IndexModel : RolePageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }


        public class RoleModel : IdentityRole
        {
            public string[] Claims { get; set; }
        }
        public List<RoleModel> Roles { get; set; }
        
        public async Task OnGet()
        {
            var r  = await _roleManager.Roles.OrderBy(role => role.Name).ToListAsync();
            Roles = new List<RoleModel>();
            foreach (var item in r)
            {
                var claims = await _roleManager.GetClaimsAsync(item);
                
                var roleModel = new RoleModel()
                {
                    Name = item.Name,
                    Id =  item.Id,
                    Claims = claims.Select(c => c.Type + "=" + c.Value).ToArray()
                };
                Roles.Add(roleModel);
            }
        }

        public void OnPost()
        {
            RedirectToPage();
        }
    }
}
