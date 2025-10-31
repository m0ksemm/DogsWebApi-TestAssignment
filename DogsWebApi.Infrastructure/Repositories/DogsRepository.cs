using DogsWebApi.Core.Domain.Entities;
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
    public class DogsRepository : EfRepository<Dog>, IDogsRepository
    {
        public DogsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _appDbContext.Dogs.AnyAsync(d => d.Name.ToLower() == name.ToLower());
        }
    }
}
