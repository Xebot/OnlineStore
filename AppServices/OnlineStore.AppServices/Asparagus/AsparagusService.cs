using OnlineStore.Contracts;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.AppServices.Asparagus
{
    public sealed class AsparagusService : IAsparagusService
    {
        private readonly IAsparagusLoverRepository _repository;

        public AsparagusService(IAsparagusLoverRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<AsparagusLoverDto>> GetList()
        {
            var result = await _repository.GetAllAsync();

            return result.Select(x => new AsparagusLoverDto
            {
                DateTime = x.DateTime,
                EatCount = x.AsparagusEatCount,
                Email = x.Email,
                Name = x.Name,
            }).ToArray();
        }

        public async Task ProcessAsparagusLoverAsync(string email, string name)
        {
            var existingUser = await _repository.GetByEmailAsync(email);

            if (existingUser != null)
            {
                existingUser.AsparagusEatCount++;
            }
            else
            {
                existingUser = new Domain.Entities.AsparagusLover
                {
                    Email = email,
                    Name = name,
                    AsparagusEatCount = 1,
                    DateTime = DateTime.UtcNow
                };
            }

            await _repository.AddOrUpdateAsync(existingUser);
        }
    }
}
