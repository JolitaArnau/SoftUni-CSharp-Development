using SIS.WebServer.Results;

namespace IRunesWebApp.Controllers
{
    using SIS.HTTP.Contracts;


    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username == null)
            {
                return this.View();
            }

            this.ViewBag["username"] = username;

            return this.View("IndexLoggedIn");
        }
    }
}