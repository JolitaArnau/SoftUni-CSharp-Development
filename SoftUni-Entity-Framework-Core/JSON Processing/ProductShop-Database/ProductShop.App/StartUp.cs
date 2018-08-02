namespace ProductShop.App
{
    using Data;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new ProductShopContext();

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();

            var jSonProcessor = new JsonProcessor();

            jSonProcessor.SeedData(context);
            
            jSonProcessor.ExportData();
        }
    }
}