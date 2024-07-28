using LearnPageRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnPageRazor.Areas.Admin.Pages.User
{
    public class UserPageModel : PageModel
    {
        protected readonly UserManager<Models.User> _userManager;

        protected readonly MyBlogContext _context;
        
        [TempData]
        public string StatusMessage { get; set; }

        public UserPageModel(UserManager<Models.User> userManager, MyBlogContext context)
        {
            _userManager = userManager;
            _context = context;
        }
    }
}