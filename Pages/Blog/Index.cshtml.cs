using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LearnPageRazor.Models;

namespace LearnPageRazor.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly LearnPageRazor.Models.MyBlogContext _context;

        public const int ITEM_PER_PAGE = 10;
        
        [BindProperty(Name = "page", SupportsGet = true)]
        public int CurrentPage { get; set; }
        public int CountPages { get; set; }

        public IndexModel(LearnPageRazor.Models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync(string Search)
        {
            // Article = await _context.Articles.ToListAsync();

            int totalArticle = await _context.Articles.CountAsync();

            CountPages = (int)Math.Ceiling((double) (totalArticle / ITEM_PER_PAGE));

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }

            if (CurrentPage > CountPages)
            {
                CurrentPage = CountPages;
            }

            var posts = (from a in _context.Articles
                orderby a.Created descending
                select a)
                .Skip((CurrentPage - 1) * ITEM_PER_PAGE)
                .Take(ITEM_PER_PAGE);

            if (!string.IsNullOrEmpty(Search))
            {
                Article =  posts.Where(p => p.Title.Contains(Search)).ToList();
            }
            else
            {
                Article = await posts.ToListAsync();
            }
        }
    }
}
