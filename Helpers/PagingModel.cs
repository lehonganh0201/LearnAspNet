using System;

namespace LearnPageRazor.Helpers
{
    public class PagingModel
    {
        public int CurrentPage { get; set; }
        public int CountPages { get; set; }
        public Func<int?,string> generateUrl { get; set; }
    }
}