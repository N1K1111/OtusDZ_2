using Microsoft.EntityFrameworkCore;
using OtusDZ_2.Data;
using OtusDZ_2.Models;
using OtusDZ_2.Repository;
using WebApi.Entities;

namespace WebApi.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        DataContext _context;

        public EFRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T enitty)
        {
            await _context.AddAsync(enitty);
            await _context.SaveChangesAsync();
            return enitty;
        }

        public async Task<T> GetAsync(long id)
        {
            var result = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }
    }
}
