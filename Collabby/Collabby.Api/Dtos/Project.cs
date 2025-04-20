using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Collabby.Domain.Entities
{
    public class ProjectDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Objectives { get; set; } = string.Empty;

        // Relación: Un proyecto puede tener muchas notas
        public ICollection<NoteDto> Notes { get; set; } = new List<NoteDto>();
    }
}