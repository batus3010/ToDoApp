using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Status> Statuss { get; set; } = null!;

        // seed data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = "work", CategoryName = "Work" },
                new Category { CategoryId = "home", CategoryName = "Home" },
                new Category { CategoryId = "ex", CategoryName = "Exercise" },
                new Category { CategoryId = "shop", CategoryName = "Shopping" },
                new Category { CategoryId = "call", CategoryName = "Contact" },
				new Category { CategoryId = "other", CategoryName = "Other" }

			);

            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "open", StatusName = "Open" },
                new Status { StatusId = "closed", StatusName = "Completed" }
            );

			modelBuilder.Entity<ToDo>()
			.Property(t => t.Id)
			.ValueGeneratedOnAdd();  // Ensure that Id is auto-generated
		}
    }
}
