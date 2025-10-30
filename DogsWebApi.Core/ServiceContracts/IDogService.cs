using DogsWebApi.Core.DTO;
using DogsWebApi.Core.ServiceContracts.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogsWebApi.Core.ServiceContracts
{
    public interface IDogService
    {
        Task<IEnumerable<DogResponse>> GetAllAsync(DogQueryParams query);
        Task<DogResponse> CreateAsync(DogAddRequest dogAddRequest);
    }
}
