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
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repo;

        public NoteService(INoteRepository repo) => _repo = repo;

        public async Task<IEnumerable<Note>> GetAllAsync() =>
            await _repo.GetAllAsync();

        public async Task<Note?> GetByIdAsync(int id) =>
            await _repo.GetByIdAsync(id);

        public async Task<Note> CreateAsync(Note note) =>
            await _repo.CreateAsync(note);

        public async Task<bool> UpdateAsync(Note note)
        {
            await _repo.UpdateAsync(note);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}