namespace SIS.TestApp
{
    using Framework;
    using Framework.Routers;
    using WebServer;

    public class Program
    {
        public static void Main()
        {
            Server server = new Server(8000, new ControllerRouter());

            MvcEngine.Run(server);
        }
    }
}