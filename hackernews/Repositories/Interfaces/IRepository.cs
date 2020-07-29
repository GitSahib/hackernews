using System.Threading.Tasks;

namespace hackernews.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T> GetByIdAsync(int id);
    }
}