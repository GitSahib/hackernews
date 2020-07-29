using hackernews.Enums;

namespace hackernews.Extensions
{
    public interface IPageRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string search { get; set; }
        public string sortBy { get; set; }
        public SortType sortType { get; set; }
    }
}