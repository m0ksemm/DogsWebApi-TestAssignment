using DogsWebApi.Core.Domain.Entities;
using DogsWebApi.Core.Domain.RepositoryContracts;
using DogsWebApi.Core.DTO;
using DogsWebApi.Core.Helpers;
using DogsWebApi.Core.ServiceContracts;
using DogsWebApi.Core.ServiceContracts.QueryParams;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq.Dynamic.Core;

namespace DogsWebApi.Core.Services
{
    public class DogService : IDogService
    {
        private readonly IDogsRepository _repo;
        private readonly ILogger<DogService> _logger;
        private static readonly HashSet<string> AllowedSorts = new() { "name", "color", "tail_length", "weight" };

        public DogService(IDogsRepository repo, ILogger<DogService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IEnumerable<DogResponse>> GetAllAsync(DogQueryParams query)
        {
            var q = _repo.Query().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(query.Attribute))
            {
                var attr = query.Attribute.Trim().ToLower();
                if (!AllowedSorts.Contains(attr))
                    throw new ArgumentException($"Invalid sort attribute: {query.Attribute}");

                string prop = attr switch
                {
                    "name" => nameof(Dog.Name),
                    "color" => nameof(Dog.Color),
                    "tail_length" => nameof(Dog.TailLength),
                    "weight" => nameof(Dog.Weight),
                    _ => nameof(Dog.Name)
                };

                var order = string.Equals(query.Order, "desc", StringComparison.OrdinalIgnoreCase) ? "descending" : "ascending";

                q = q.OrderBy($"{prop} {order}");
            }

            if (query.PageNumber <= 0) query.PageNumber = 1;
            if (query.PageSize <= 0) query.PageSize = 100;

            var skipped = (query.PageNumber - 1) * query.PageSize;
            var page = await q.Skip(skipped).Take(query.PageSize).ToListAsync();

            return page.Select(d => new DogResponse
            {
                Name = d.Name,
                Color = d.Color,
                TailLength = d.TailLength,
                Weight = d.Weight
            });
        }

        public async Task<DogResponse> CreateAsync(DogAddRequest dogAddRequest)
        {
            ValidationHelper.ValidateModel(dogAddRequest);


            var exists = await _repo.ExistsByNameAsync(dogAddRequest.Name);
            if (exists) throw new InvalidOperationException($"Dog with name '{dogAddRequest.Name}' already exists");

            var entity = new Dog
            {
                Name = dogAddRequest.Name.Trim(),
                Color = dogAddRequest.Color?.Trim() ?? string.Empty,
                TailLength = dogAddRequest.TailLength,
                Weight = dogAddRequest.Weight
            };

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return new DogResponse
            {
                Name = entity.Name,
                Color = entity.Color,
                TailLength = entity.TailLength,
                Weight = entity.Weight
            };
        }
    }
}
