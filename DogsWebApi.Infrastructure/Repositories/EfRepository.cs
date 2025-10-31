using DogsWebApi.Core.Domain.RepositoryContracts;
using DogsWebApi.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsWebApi.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext;
        public EfRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext; 
        }
        public async Task AddDog(T entity) => await _appDbContext.Set<T>().AddAsync(entity);
        public IQueryable<T> Query() => _appDbContext.Set<T>().AsQueryable();
        public async Task<IEnumerable<T>> GetAllAsync() => await _appDbContext.Set<T>().ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _appDbContext.Set<T>().FindAsync(id);
        public async Task SaveChangesAsync() => await _appDbContext.SaveChangesAsync();
    }
}
