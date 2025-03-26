using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Collabby.Domain.Entities;
using Collabby.infrastructure.Context;

namespace Collabby.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EstudiantesController : ControllerBase
{
    private readonly CollabbyContext _context;

    public EstudiantesController(CollabbyContext context)
    {
        _context = context;
    }

    [HttpGet(nameof(GetAll))]
    public async Task<IActionResult> GetAll(string filter = "")
    {

        var entities = await _context.Notes.ToListAsync();



        var list = entities.Select(entity => new NoteDto
        {
            NoteId = entity.NoteId,
            UserId = entity.UserId,
            Title = entity.Title,
            Content = entity.Content,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
        }).ToList();

        return Ok(list);
    }

    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var entitydb = await _context.Notes.FindAsync(id);
        if (entitydb == null)
        {
            return BadRequest("Not found");
        }

        var entity = new NoteDto
        {
            NoteId = entitydb.NoteId,
            UserId = entitydb.UserId,
            Title = entitydb.Title,
            Content = entitydb.Content,
            CreatedAt = entitydb.CreatedAt,
            UpdatedAt = entitydb.UpdatedAt,
        };

        return Ok(entity);
    }

    [HttpPost("Add")]
    public async Task<IActionResult> Create([FromBody] NoteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("The entity is invalid");
        }

        var entity = new Note
        {
            NoteId = dto.NoteId,
            UserId = dto.UserId,
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
        };

        _context.Notes.Add(entity);
        await _context.SaveChangesAsync();
        return Ok(new { success = true, message = "Created successfully!" });
    }

    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update([FromBody] NoteDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("The entity is invalid");
        }

        var entity = new Note
        {
            NoteId = dto.NoteId,
            UserId = dto.UserId,
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
        };

        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EstudianteExists(entity.NoteId))
            {
                return BadRequest("Not found");
            }
            else
            {
                throw;
            }
        }

        return Ok(new { success = true, message = "Updated successfully!" });
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entityDb = await _context.Notes.FindAsync(id);
        if (entityDb == null)
        {
            return BadRequest("Not found");
        }

        _context.Notes.Remove(entityDb);
        await _context.SaveChangesAsync();
        return Ok(new { success = true, message = "Deleted successfully!" });
    }

    private bool EstudianteExists(int id)
    {
        return _context.Notes.Any(e => e.NoteId == id);
    }
}