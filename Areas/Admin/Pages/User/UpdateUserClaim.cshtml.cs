using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LearnPageRazor.Areas.Admin.Pages.User
{
    public class UpdateUserClaim : PageModel
    {
        private readonly MyBlogContext _context;
        private readonly UserManager<Models.User> _userManager;

        public UpdateUserClaim(MyBlogContext context, UserManager<Models.User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        [TempData]
        public string StatusMessage { get; set; }
        
        public class InputModel
        {
            [DisplayName("Loại đặc tính")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} kí tự")]
            public string ClaimType { get; set; }
            
            [DisplayName("Giá trị đặc tính")]
            [Required(ErrorMessage = "Phải nhập {0}")]
            [StringLength(256, MinimumLength = 3, ErrorMessage = "{0} phải dài {2} đến {1} kí tự")]
            public string ClaimValue { get; set; }
        }
        
        [BindProperty]
        public InputModel Input { get; set; }

        public NotFoundObjectResult OnGet() => NotFound("Access Denied");
        
        public Models.User User { get; set; }
        
        public IdentityUserClaim<string> UserClaim { get; set; }

        public async Task<IActionResult> OnGetAddClaimAsync(string userId)
        {
            User = await _userManager.FindByIdAsync(userId);
            if (User == null)
            {
                return NotFound("Not Found User");
                
            }
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostAddClaimAsync(string userId)
        {
            User = await _userManager.FindByIdAsync(userId);
            if (User == null)
            {
                return NotFound("Not Found User");
                
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var claims = await _context.UserClaims.Where(c=>c.UserId.Equals(userId)).ToListAsync();

            if (claims.Any(c => c.ClaimType.Equals(Input.ClaimType) && c.ClaimValue.Equals(Input.ClaimValue)))
            {
                ModelState.AddModelError(string.Empty, "Đặc tính này đã có");
                return Page();
            }

            await _userManager.AddClaimAsync(User, new Claim(Input.ClaimType, Input.ClaimValue));

            StatusMessage = $"Bạn đã thêm đặc tính riêng cho {User.UserName}";
            
            return RedirectToPage("./AddRole", new {userId = userId});
        }
        
        public async Task<IActionResult> OnGetEditClaimAsync(int? claimId)
        {
            if (claimId == null)
            {
                return NotFound("Not Found Claim");
            }

            UserClaim = _context.UserClaims.Where(c => c.Id == claimId).FirstOrDefault();
            User = await _userManager.FindByIdAsync(UserClaim.UserId);
            if (User == null)
            {
                return NotFound("Not Found User");
            }

            Input = new InputModel()
            {
                ClaimType = UserClaim.ClaimType,
                ClaimValue = UserClaim.ClaimValue
            };
            
            return Page();
        }
        
        public async Task<IActionResult> OnPostEditClaimAsync(int? claimId)
        {
            if (claimId == null)
            {
                return NotFound("Not Found Claim");
            }

            UserClaim = _context.UserClaims.Where(c => c.Id == claimId).FirstOrDefault();
            User = await _userManager.FindByIdAsync(UserClaim.UserId);
            if (User == null)
            {
                return NotFound("Not Found User");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_context.UserClaims.Any(c =>
                c.UserId == User.Id && c.ClaimType == Input.ClaimType && c.ClaimValue == Input.ClaimValue &&
                c.Id != UserClaim.Id))
            {
                ModelState.AddModelError(string.Empty, "Đặc tính này đã tồn tại");
                return Page();
            }

            UserClaim.ClaimType = Input.ClaimType;
            UserClaim.ClaimValue = Input.ClaimValue;

            await _context.SaveChangesAsync();
            StatusMessage = $"Bạn vừa cập nhật {UserClaim.ClaimType}";
            
            return Page();
        }
    }
}