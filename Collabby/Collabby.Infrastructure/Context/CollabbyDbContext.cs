using System.Collections.Generic;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Collabby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Collabby.infrastructure.Context
{
    public class CollabbyContext : DbContext
    {
        public CollabbyContext(DbContextOptions<CollabbyContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }



    }

}