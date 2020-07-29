using hackernews.Models;
using hackernews.Repositories.Interfaces;
using hackernews.ViewModels.Request;
using hackernews.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace hackernews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IStoryRepository _repo;
        public StoryController(IStoryRepository repository)
        {
            _repo = repository;
        }
        [HttpPost]
        public async Task<Pageable<Story>> Index(StoryClientRequest request)
        {
           return await _repo.GetAllByTypeAsync(request);
        }
    }
}
