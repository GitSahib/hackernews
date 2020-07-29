using NUnit.Framework;
using hackernews.Repositories.Implementations;
using System.Net.Http;
using Microsoft.Extensions.Caching.Memory;
using hackernews.Models;
using System.Threading.Tasks;
using hackernews.ViewModels.Response;
using System.Linq;
using hackernews.Repositories.Interfaces;
using System;
using hackernews.ViewModels.Request;
using hackernews.Enums;
using System.IO;
using System.Text;
using System.Threading;

namespace hackernews.tests
{
    public class StoryRepositoryTests
    {
        IStoryRepository repo;
        IMemoryCache cache;
        HttpClient client;
        int testId;
        [SetUp]
        public void Setup()
        {
            client = new HttpClient(new HttpMessageHandlerStub())
            {
                BaseAddress = new Uri("http://data")
            };
            cache = new MemoryCache(new MemoryCacheOptions());
            repo = new StoryRepository(client, cache);
            testId = 23975255;
        }

        [Test]
        public void GetByIdAsync()
        {
            Story s = Task.Run(() => repo.GetByIdAsync(0)).Result;
            Assert.IsNull(s);
            s = Task.Run(() => repo.GetByIdAsync(testId)).Result;
            TestStory(s);
        }

        [Test]
        public void GetByTypeAsync()
        {
            StoryClientRequest request = new StoryClientRequest
            {
                page = 0,
                pageSize = 10,
                type = StoryType.NewStories.ToString()
            };
            Pageable<Story> stories = Task.Run(async () => await repo.GetAllByTypeAsync(request)).Result;
            Assert.IsTrue(stories.page == 0);
            Assert.IsTrue(stories.content.Count() == 10);
            Assert.IsTrue(stories.pageSize == 10);
            Assert.IsTrue(stories.totalCount > 10);
        }
        [Test]
        public void GetByTypeAndSearchAsync()
        {
            StoryClientRequest request = new StoryClientRequest
            {
                page = 0,
                pageSize = 10,
                type = StoryType.NewStories.ToString(),
                search = "test"
            };
            Pageable<Story> stories = Task.Run(async () => await repo.GetAllByTypeAsync(request)).Result;
            Assert.IsTrue(stories.page == 0);
            Assert.IsTrue(stories.content.Count() == 10);
            Assert.IsTrue(stories.pageSize == 10);
            Assert.IsTrue(stories.totalCount > 10);
        }
        [Test]
        public void CacheTest()
        {
            StoryClientRequest request = new StoryClientRequest
            {
                page = 0,
                pageSize = 1,
                type = StoryType.NewStories.ToString(),
                search = ""
            };
            Pageable<Story> stories = Task.Run(async () => await repo.GetAllByTypeAsync(request)).Result;
            Assert.IsTrue(cache.TryGetValue(stories.content.FirstOrDefault().id, out Story s));
            TestStory(s);
        }

        private void TestStory(Story s)
        {
            Assert.IsNotNull(s);
            Assert.IsNotNull(s.id);
            Assert.IsNotNull(s.link);
            Assert.IsNotNull(s.title);
        }
    }
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => {
                string json = "";
                string url = $"data/{request.RequestUri.ToString().Replace("http://data/", "")}";
                if (File.Exists(url)) 
                    json = File.ReadAllText(url);
                var message = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };
                return message;
            });
        }
    }
}