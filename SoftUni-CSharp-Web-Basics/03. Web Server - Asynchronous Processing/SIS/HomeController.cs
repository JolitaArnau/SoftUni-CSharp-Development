using SIS.HTTP.Contracts;
using SIS.HTTP.Enums;
using SIS.WebServer.Results;

namespace SIS
{
    public class HomeController
    {
        public IHttpResponse Index()
        {
            var content = "<h1>Hello World</h1>";
            
            return new HtmlResult(content, HttpResponseStatusCode.Ok);
        }
    }
}