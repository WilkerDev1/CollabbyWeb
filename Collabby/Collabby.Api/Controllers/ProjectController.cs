using Microsoft.AspNetCore.Mvc;
using Collabby.Api.Dtos.Project;
using Collabby.Application.Interfaces;
using Collabby.Domain.Entities;
using Collabby.Application.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _svc;
    public ProjectController(IProjectService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> Get()
    {
        var projects = await _svc.GetAllAsync();
        var dtos = projects.Select(p => new ProjectDto
        {
            ProjectId = p.ProjectId,
            Name = p.Name,
            Description = p.Description,
            Objectives = p.Objectives
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> Get(int id)
    {
        var p = await _svc.GetByIdAsync(id);
        if (p == null) return NotFound();
        return Ok(new ProjectDto
        {
            ProjectId = p.ProjectId,
            Name = p.Name,
            Description = p.Description,
            Objectives = p.Objectives
        });
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Post(CreateProjectDto dto)
    {
        var p = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            Objectives = dto.Objectives
        };
        var created = await _svc.CreateAsync(p);
        return CreatedAtAction(nameof(Get), new { id = created.ProjectId },
                               new ProjectDto
                               {
                                   ProjectId = created.ProjectId,
                                   Name = created.Name,
                                   Description = created.Description,
                                   Objectives = created.Objectives
                               });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateProjectDto dto)
    {
        if (id != dto.ProjectId) return BadRequest();
        var p = new Project
        {
            ProjectId = dto.ProjectId,
            Name = dto.Name,
            Description = dto.Description,
            Objectives = dto.Objectives
        };
        await _svc.UpdateAsync(p);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}
