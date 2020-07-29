using hackernews.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hackernews.ViewModels.Response
{
    public class Pageable<T>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public IEnumerable<T> content { get; set; }
        public int filtered { get; internal set; }
        public int pageCount { get; set; }
    }
}
