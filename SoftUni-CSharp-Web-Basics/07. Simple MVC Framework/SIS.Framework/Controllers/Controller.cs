namespace SIS.Framework.Controllers
{
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using ActionsResults;
    using ActionsResults.Contracts;
    using HTTP.Requests;
    using Views;
    using Utilities;
    using Services;

    public abstract class Controller
    {
        private const string AuthCookieKey = "irunes_auth";

        protected Controller()
        {
            this.UserCookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }

        private IUserCookieService UserCookieService { get; set; }

        public IHttpRequest Request { get; set; }

        private Dictionary<string, string> ViewBag { get; set; }

        protected IViewable View([CallerMemberName] string caller = "")
        {
            var controllerName = ControllerUtilities.GetControllerName(this);

            var fullQualifiedName = ControllerUtilities.GetViewFullyQualifiedName(controllerName, caller);

            var view = new View(fullQualifiedName, this.ViewBag);

            return new ViewResult(view);
        }


        protected IRedirectable RedirectToAction(string redirectUrl)
            => new RedirectResult(redirectUrl);

        protected string User
        {
            get
            {
                if (!this.Request.Cookies.ContainsCookie(AuthCookieKey))
                {
                    return null;
                }

                var cookie = this.Request.Cookies.GetCookie(AuthCookieKey);
                var cookieContent = cookie.Value;
                var userName = this.UserCookieService.GetUserData(cookieContent);
                return userName;
            }
        }
    }
}