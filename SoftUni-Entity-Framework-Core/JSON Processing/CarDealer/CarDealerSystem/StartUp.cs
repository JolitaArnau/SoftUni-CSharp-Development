using CarDealer.Data;

namespace CarDealer.App
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new CarDealerContext();

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            var jsonProcessor = new JsonProcessor();
            
            jsonProcessor.SeedData(context);
            
            jsonProcessor.ExportData(context);
        }
    }
}