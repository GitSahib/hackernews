using hackernews.Models;
using hackernews.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using hackernews.Extensions;
using Microsoft.Extensions.Caching.Memory;
using System;
using hackernews.ViewModels.Response;
using hackernews.Enums;
using hackernews.ViewModels.Request;
using System.IO;

namespace hackernews.Repositories.Implementations
{
    public class StoryRepository : IStoryRepository
    {
        private readonly HttpClient client;
        private readonly IMemoryCache cache;
        public StoryRepository(HttpClient httpClient, IMemoryCache mcache)
        {
            client = httpClient;
            cache = mcache;
        }

        public async Task<IEnumerable<int>> GetIdsByTypeAsync(string type)
        {
            bool parsed = Enum.TryParse(typeof(StoryType), type, out var storyType);
            storyType = parsed ? storyType : StoryType.TopStories;
            var response = await client.GetAsync($"{storyType.ToString().ToLower()}.json");
            var storiesResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<int>>(storiesResponse);            
        }

        public async Task<Story> GetByIdAsync(int id)
        {
            return await cache.GetOrCreateAsync(id,
                async cacheEntry =>
                {
                    var response = await client.GetAsync($"item/{id}.json");
                    if (response.IsSuccessStatusCode)
                    {
                        var responsJSON = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<Story>(responsJSON);

                    }
                    return null;
                });
        }

        public async Task<Pageable<Story>> GetAllByTypeAsync(StoryClientRequest request)
        {
            var ids = await GetIdsByTypeAsync(request.type);
            var total = ids.Count();
            var requestedPage = request.page;
            /*Do not load all if there is no search*/
            if (string.IsNullOrEmpty(request.search))
            {
                ids = ids.Page(request.page, request.pageSize);
                request.page = 0;
            }

            /*load the id details in separate threads*/
            IEnumerable<Story> stories = (await Task.WhenAll(ids.Select(GetByIdAsync)));
            /*now page the result*/
            Pageable<Story> pageable = stories.Page(request);
            /*fix the counts if no search*/
            if (string.IsNullOrEmpty(request.search))
            {
                pageable.filtered = pageable.totalCount = total;
                pageable.pageCount = pageable.filtered / pageable.pageSize;
                pageable.page = requestedPage;
            }
            return pageable;
        }
    }
}
