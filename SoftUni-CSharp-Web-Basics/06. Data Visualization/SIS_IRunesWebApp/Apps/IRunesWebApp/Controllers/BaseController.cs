namespace IRunesWebApp.Controllers
{
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Collections.Generic;
    using SIS.HTTP.Enums;
    using SIS.WebServer.Results;
    using SIS.HTTP.Cookies;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using Data;
    using Services;
    
    public abstract class BaseController
    {
        private const string ViewsFolderName = "Views";

        private const string ControllerDefaultName = "Controller";

        private const string HtmlFileExtension = ".html";

        private const string LayoutViewFileName = "_Layout";

        private const string RenderBodyConstant = "@RenderBody()";
        
        protected IRunesContext Db { get; set; }
        
        protected IHashService HashService;
        
        protected IUserCookieService CookieService;
        
        protected bool IsUserAuthenticated { get; set; } = false;

        protected IDictionary<string, string> ViewBag { get; set; }

        public BaseController()
        {
            this.Db = new IRunesContext();
            this.HashService = new HashService();
            this.CookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }
        
        public bool IsAuthenticated(IHttpRequest request)
        {
            return request.Session.ContainsParameter("username");
        }
        
        private string GetControllerName => this.GetType().Name.Replace(ControllerDefaultName, string.Empty);

        public void SignInUser(
            string username,
            IHttpResponse response, 
            IHttpRequest request)
        {
            request.Session.AddParameter("username", username);
            var userCookieValue = this.CookieService.GetUserCookie(username);
            response.Cookies.Add(new HttpCookie("auth-irunes", userCookieValue));
        }

        protected string GetUsername(IHttpRequest request)
        {
            
            if (!request.Cookies.ContainsCookie("auth-irunes"))
            {
                return null;
            }

            this.IsUserAuthenticated = true;

            var cookie = request.Cookies.GetCookie("auth-irunes");
            var cookieContent = cookie.Value;
            var userName = this.CookieService.GetUserData(cookieContent);
            return userName;
        }

        protected IHttpResponse View([CallerMemberName] string viewName = "")
        {
            var filePath =
                $"{ViewsFolderName}/{this.GetControllerName}/{viewName}{HtmlFileExtension}";

            var layoutView = $"{ViewsFolderName}/{LayoutViewFileName}{HtmlFileExtension}";

            if (!File.Exists(filePath))
            {
                return new BadRequestResult(
                    $"View {viewName} not found.",
                    HttpResponseStatusCode.NotFound);
            }

            var viewContent = BuildViewContent(filePath);


            var viewLayout = File.ReadAllText(layoutView);
            var view = viewLayout.Replace(RenderBodyConstant, viewContent);

            var response = new HtmlResult(view, HttpResponseStatusCode.Ok);

            return response;
        }
        
        private string BuildViewContent(string filePath)
        {
            var viewContent = File.ReadAllText(filePath);

            foreach (var viewBagKey in ViewBag.Keys)
            {
                var dynamicDataPlaceholder = $"{{{{{viewBagKey}}}}}";
                if (viewContent.Contains(dynamicDataPlaceholder))
                {
                    viewContent = viewContent.Replace(
                        dynamicDataPlaceholder,
                        this.ViewBag[viewBagKey]);
                }
            }

            return viewContent;
        }
    }
}