using OtusDZ_2.Models;
using WebApi.Entities;

namespace OtusDZ_2.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T?> GetAsync(long id);
        public Task<T> AddAsync(T enitty);
    }
}
