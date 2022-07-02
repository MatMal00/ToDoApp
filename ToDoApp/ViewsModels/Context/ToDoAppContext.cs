using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp
{
    internal class ToDoAppContext: DbContext
    {
        public DbSet<ToDoTaskModel> ToDo { get; set; }
        public DbSet<DoneTaskModel> Done { get; set; }
        public DbSet<DeletedTaskModel> Deleted { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }


        public string ConnectionString { get; }

        public ToDoAppContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(ConnectionString);
        }
    }
}
