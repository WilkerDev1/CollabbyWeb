using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collabby.Application.Interfaces;
using Collabby.Domain.Entities;
using Collabby.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Collabby.Infrastructure.Repositories
{
    public class NoteService : INoteRepository
    {
        private readonly CollabbyContext _ctx;
        public NoteService(CollabbyContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<Note>> GetAllAsync() =>
          await _ctx.Notes.ToListAsync();

        public async Task<Note?> GetByIdAsync(int id) =>
          await _ctx.Notes.FindAsync(id);

        public async Task<Note> CreateAsync(Note note)
        {
            _ctx.Notes.Add(note);
            await _ctx.SaveChangesAsync();
            return note;
        }

        public async Task UpdateAsync(Note note)
        {
            _ctx.Notes.Update(note);
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var n = await _ctx.Notes.FindAsync(id);
            if (n != null)
            {
                _ctx.Notes.Remove(n);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}