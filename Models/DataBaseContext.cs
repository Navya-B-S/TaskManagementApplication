using Microsoft.EntityFrameworkCore;

namespace TaskManagementApplication.Models
{
    public class DataBaseContext : DbContext
    {
        //add ctor snippet and create a constructor that accepts DbContextOptions and check if database exists or not and create it if it does not exist
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            CreateDatabase();
        }

        //add default constructor without parameters and check if tasks table exists or not and create it if it does not exist
        public DataBaseContext()
        {
            CreateTasksTable();
        }


        //override the OnConfiguring method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //use the SQLite database provider
            optionsBuilder.UseSqlite(@"Data Source=./TaskManagementApplication.db");
        }

        //define the Tasks table
        public DbSet<Task> Tasks { get; set; }

        //override the OnModelCreating method to configure the model using Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //define the primary key for the Tasks table
            modelBuilder.Entity<Task>().HasKey(t => t.TaskId);
        }

        //check if Tasks table exists in the database
        private bool TasksTableExists()
        {
            //get the database connection
            using (var connection = Database.GetDbConnection())
            {
                //open the connection
                connection.Open();

                //get the database command
                using (var command = connection.CreateCommand())
                {
                    //set the command text
                    command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Tasks'";

                    //execute the command and get the result
                    using (var reader = command.ExecuteReader())
                    {
                        //return true if the table exists
                        return reader.Read();
                    }
                }
            }
        }

        //create the Tasks table in the database if it does not exist
        private void CreateTasksTable()
        {
            //check if the Tasks table exists
            if (!TasksTableExists())
            {
                //create the Tasks table
                Database.ExecuteSqlRaw("CREATE TABLE Tasks (TaskId INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT NOT NULL, Description TEXT NOT NULL, DueDate TEXT NOT NULL, Priority TEXT NOT NULL, Status TEXT NOT NULL)");
            }
        }

        //Create the database if it does not exist
        private void CreateDatabase()
        {
            //check if the database exists
            if (!Database.CanConnect())
            {
                //create the database
                Database.EnsureCreated();
            }
        }

    }
}
