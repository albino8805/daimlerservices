using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Daimler.data.Models
{
    public class DAIMLERContext : DbContext
    {
        public DAIMLERContext(DbContextOptions<DAIMLERContext> options) : base(options) { }

        public virtual DbSet<Module> Modules { get; set; }

        public virtual DbSet<Action> Actions { get; set; }

        public virtual DbSet<Profile> Profiles { get; set; }

        public virtual DbSet<ModuleAction> ModuleActions { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<State> States { get; set; }

        public virtual DbSet<Town> Towns { get; set; }

        public virtual DbSet<Folder> Folders { get; set; }
    }
}
