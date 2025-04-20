using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Collabby.Application.Services.Interfaces;

namespace Collabby.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects.Select(p => new ProjectDto
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Objectives = p.Objectives
                // No incluyo Notes por simplicidad (o puedes mapearlas si las necesitas)
            });
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var p = await _projectRepository.GetByIdAsync(id);
            if (p == null) return null;

            return new ProjectDto
            {
                ProjectId = p.ProjectId,
                Name = p.Name,
                Description = p.Description,
                Objectives = p.Objectives
            };
        }

        public async Task<ProjectDto> CreateAsync(ProjectDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description,
                Objectives = dto.Objectives
            };

            var created = await _projectRepository.CreateAsync(project);
            dto.ProjectId = created.ProjectId;
            return dto;
        }

        public async Task<bool> UpdateAsync(ProjectDto dto)
        {
            var project = new Project
            {
                ProjectId = dto.ProjectId,
                Name = dto.Name,
                Description = dto.Description,
                Objectives = dto.Objectives
            };

            return await _projectRepository.UpdateAsync(project);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _projectRepository.DeleteAsync(id);
        }
    }
}