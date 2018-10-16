namespace SIS.TestApp.Controllers
{
    using Framework.ActionsResults.Base;
    using SIS.Framework.Controllers;

    public class HomeController : Controller
    {
        public IActionResult Index() 
        {
            return this.View();
        }
    }
}