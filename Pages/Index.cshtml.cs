using System.Linq;
using LearnPageRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly MyBlogContext _context;

    public IndexModel(ILogger<IndexModel> logger, MyBlogContext context)
    {
        _logger = logger;
        _context = context;
    }

    public void OnGet()
    {
        var posts = (from a in _context.Articles
            orderby a.Created descending
            select a).ToList();

        ViewData["Posts"] = posts;
    }
}

/*
 * dotnet aspnet-codegenerator razorpage -m LearnPageRazor.Models.Article -dc LearnPageRazor.Models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries
 */