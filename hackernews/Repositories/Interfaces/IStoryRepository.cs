using hackernews.Extensions;
using hackernews.Models;
using hackernews.ViewModels.Request;
using hackernews.ViewModels.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hackernews.Repositories.Interfaces
{
    public interface IStoryRepository : IRepository<Story>
    {
        public Task<IEnumerable<int>> GetIdsByTypeAsync(string type);
        public Task<Pageable<Story>> GetAllByTypeAsync(StoryClientRequest request);
    }
}