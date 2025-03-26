using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabby.Domain.Entities
{
    public class NoteDto
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }  // Clave foránea
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Relación: Una nota pertenece a un usuario
        public UserDto? User { get; set; }
    }
}

