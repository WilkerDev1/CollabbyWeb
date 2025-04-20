using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Collabby.Application.Interfaces;
using Collabby.Application.Services.Interfaces;
using Collabby.Domain.Entities;

namespace Collabby.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user) =>
            await _repo.CreateAsync(user);

        public async Task<bool> UpdateAsync(User user)
        {
            await _repo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}