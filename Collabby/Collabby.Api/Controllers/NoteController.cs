using Microsoft.AspNetCore.Mvc;
using Collabby.Api.Dtos.Note;
using Collabby.Application.Interfaces;
using Collabby.Domain.Entities;
using Collabby.Infrastructure.Repositories;

[ApiController]
[Route("api/[controller]")]
public class NoteController : ControllerBase
{
    private readonly INoteService _svc;
    public NoteController(INoteService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NoteDto>>> Get()
    {
        var notes = await _svc.GetAllAsync();
        var dtos = notes.Select(n => new NoteDto
        {
            NoteId = n.NoteId,
            UserId = n.UserId,
            ProjectId = n.ProjectId,
            Title = n.Title,
            Content = n.Content,
            CreatedAt = n.CreatedAt,
            UpdatedAt = n.UpdatedAt
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NoteDto>> Get(int id)
    {
        var n = await _svc.GetByIdAsync(id);
        if (n == null) return NotFound();
        return Ok(new NoteDto
        {
            NoteId = n.NoteId,
            // … mapea el resto …
        });
    }

    [HttpPost]
    public async Task<ActionResult<NoteDto>> Post(CreateNoteDto dto)
    {
        var n = new Note
        {
            UserId = dto.UserId,
            ProjectId = dto.ProjectId,
            Title = dto.Title,
            Content = dto.Content
        };
        var created = await _svc.CreateAsync(n);
        return CreatedAtAction(nameof(Get), new { id = created.NoteId },
                               new NoteDto { /* … */ });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateNoteDto dto)
    {
        if (id != dto.NoteId) return BadRequest();
        var n = new Note
        {
            NoteId = dto.NoteId,
            UserId = dto.UserId,
            ProjectId = dto.ProjectId,
            Title = dto.Title,
            Content = dto.Content,
            UpdatedAt = DateTime.UtcNow
        };
        await _svc.UpdateAsync(n);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}