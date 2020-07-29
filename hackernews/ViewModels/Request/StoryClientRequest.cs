using hackernews.Enums;
using hackernews.Extensions;

namespace hackernews.ViewModels.Request
{
    public class StoryClientRequest : IPageRequest
    {
        public string type { get; set; }
        public int page { get; set; }
        public int pageSize { get ; set ; }
        public string search { get ; set ; }
        public string sortBy { get ; set ; }
        public SortType sortType { get ; set ; }
    }
}
