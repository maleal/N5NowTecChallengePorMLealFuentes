using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N5Now.Core.Entities;

namespace N5Now.Infrastructure.DataBaseIttion
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Permissions> permissions { get; set; }
        public DbSet<PermissionType> permissionTypes { get; set; }

    }
}
