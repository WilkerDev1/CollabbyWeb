using System.Collections.Generic;
using System.Threading.Tasks;
using Collabby.Domain.Entities;
using Collabby.infrastructure.Context;
using Collabby.Infrastructure.Context;
using Collabby.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Collabby.Infrastructure.Repositories
{
    public class ProjectService : IProjectRepository
    {
        private readonly CollabbyContext _ctx;
        public ProjectService(CollabbyContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Project>> GetAllAsync() =>
            await _ctx.Projects.ToListAsync();

        public async Task<Project?> GetByIdAsync(int id) =>
            await _ctx.Projects.FindAsync(id);

        public async Task<Project> CreateAsync(Project project)
        {
            _ctx.Projects.Add(project);
            await _ctx.SaveChangesAsync();
            return project;
        }

        public async Task<bool> UpdateAsync(Project project)
        {
            _ctx.Projects.Update(project);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _ctx.Projects.FindAsync(id);
            if (p != null)
            {
                _ctx.Projects.Remove(p);
                return await _ctx.SaveChangesAsync() > 0;
            }
            return false;
        }
    }