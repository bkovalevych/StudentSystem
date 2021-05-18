using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace StudentSystem
{
    using Entities;
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<StudentParent> StudentParents { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Department> Departments { get; set; }
        public StudentContext() {
            this.Database.EnsureCreated();    
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            string opt = "Data Source=" + Path.Combine(ApplicationData.Current.LocalFolder.Path, "students.db");
            optionsBuilder.UseSqlite(opt);
        }

    }
}
