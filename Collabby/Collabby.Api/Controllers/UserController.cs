using Microsoft.AspNetCore.Mvc;
using Collabby.Api.Dtos.User;
using Collabby.Application.Interfaces;
using Collabby.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _svc;
    public UserController(IUserService svc) => _svc = svc;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var users = await _svc.GetAllAsync();
        var dtos = users.Select(u => new UserDto
        {
            UserId = u.UserId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var u = await _svc.GetByIdAsync(id);
        if (u == null) return NotFound();
        return Ok(new UserDto
        {
            UserId = u.UserId,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            CreatedAt = u.CreatedAt,
            UpdatedAt = u.UpdatedAt
        });
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Post(CreateUserDto dto)
    {
        var u = new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash
        };
        var created = await _svc.CreateAsync(u);
        return CreatedAtAction(nameof(Get), new { id = created.UserId },
                               new UserDto
                               {
                                   UserId = created.UserId,
                                   FirstName = created.FirstName,
                                   LastName = created.LastName,
                                   Email = created.Email,
                                   PasswordHash = created.PasswordHash,
                                   CreatedAt = created.CreatedAt,
                                   UpdatedAt = created.UpdatedAt
                               });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateUserDto dto)
    {
        if (id != dto.UserId) return BadRequest();
        var u = new User
        {
            UserId = dto.UserId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordHash = dto.PasswordHash,
            UpdatedAt = DateTime.UtcNow
        };
        await _svc.UpdateAsync(u);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _svc.DeleteAsync(id);
        return NoContent();
    }
}
