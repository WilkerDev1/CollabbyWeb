using Collabby.Domain.Entities;
using Collabby.infrastructure.Context;
using Collabby.Infrastructure.Interfaces;

public class UserService : IUserRepository
{
    private readonly CollabbyContext _ctx;
    public UserService(CollabbyContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _ctx.Users.ToListAsync();

    public async Task<User?> GetByIdAsync(int id) =>
        await _ctx.Users.FindAsync(id);

    public async Task<User> CreateAsync(User user)
    {
        _ctx.Users.Add(user);
        await _ctx.SaveChangesAsync();
        return user;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _ctx.Users.Update(user);
        return await _ctx.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var u = await _ctx.Users.FindAsync(id);
        if (u != null)
        {
            _ctx.Users.Remove(u);
            return await _ctx.SaveChangesAsync() > 0;
        }
        return false;
    }
}
}
