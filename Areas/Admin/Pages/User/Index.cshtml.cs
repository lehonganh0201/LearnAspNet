using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LearnPageRazor.Areas.Admin.Pages.User
{
    [Authorize(Roles = "Admin,Manager")]
    public class IndexModel : UserPageModel
    {
        public IndexModel(UserManager<Models.User> userManager, MyBlogContext context) : base(userManager, context)
        {
        }

        public class UserAndRole : Models.User
        {
            public string RoleName { get; set; }
        }
        
        public List<UserAndRole> Users { get; set; }
        
        public const int ITEM_PER_PAGE = 10;
        
        [BindProperty(Name = "page", SupportsGet = true)]
        public int CurrentPage { get; set; }
        public int CountPages { get; set; }
        
        public int TotalUsers { get; set; }

        public async Task OnGet()
        {
            // Users = await _userManager.Users.OrderBy(u => u.UserName).ToListAsync();
            var qr = _userManager.Users.OrderBy(p => p.UserName);
            TotalUsers = await _context.Users.CountAsync();
            CountPages = (int) Math.Ceiling((double) (TotalUsers / ITEM_PER_PAGE));
            Console.WriteLine(CurrentPage);
            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }

            if (CurrentPage > CountPages)
            {
                CurrentPage = CountPages;
            }
            
            var qr1 = qr.Skip((CurrentPage - 1) * ITEM_PER_PAGE)
                .Take(ITEM_PER_PAGE).Select(u=> new UserAndRole()
                {
                    Id = u.Id,
                    UserName = u.UserName
                });

            Users = await qr1.ToListAsync();

            foreach (var user in Users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.RoleName = string.Join(",", roles);
            }

        }

        public void OnPost()
        {
            RedirectToPage();
        }
    }
}