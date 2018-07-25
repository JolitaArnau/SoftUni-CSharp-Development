namespace AutoMappingObjects.Services
{
   using Data;
   using Contracts;
    
    public class DatabaseInitializerService : IDatabaseInitializerService
    {
        private readonly EmployeesDbContext context;

        public DatabaseInitializerService(EmployeesDbContext context)
        {
            this.context = context;
        }

        public void InitializeDatabase()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}