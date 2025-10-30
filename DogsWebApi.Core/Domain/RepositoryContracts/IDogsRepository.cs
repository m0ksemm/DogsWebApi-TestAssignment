using DogsWebApi.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsWebApi.Core.Domain.RepositoryContracts
{
    public interface IDogsRepository : IRepository<Dog>
    {
        Task<bool> ExistsByNameAsync(string name);
    }
}
