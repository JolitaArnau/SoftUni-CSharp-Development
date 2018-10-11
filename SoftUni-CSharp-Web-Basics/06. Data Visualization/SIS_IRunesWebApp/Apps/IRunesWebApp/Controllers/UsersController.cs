namespace IRunesWebApp.Controllers
{
    using System;
    using System.Linq;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using SIS.WebServer.Results;
    using Models;
    using Services;
    
    public class UsersController : BaseController
    {
        private readonly HashService hashService;

        public UsersController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Login(IHttpRequest request) => this.View();

        public IHttpResponse PostLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashService.Hash(password);

            var user = this.Db.Users
                .FirstOrDefault(u => u.Username == username &&
                    u.Password == hashedPassword);

            if (user == null)
            {
                return new RedirectResult("/");
            }

            var response = new RedirectResult("/home/index");
            this.SignInUser(username, response, request);
            return response;
        }

        public IHttpResponse Register(IHttpRequest request) => this.View();

        public IHttpResponse PostRegister(IHttpRequest request)
        {
            var userName = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();

             //Validate
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < 4)
            {
                return new BadRequestResult("Please provide valid username with length of 4 or more characters.", HttpResponseStatusCode.BadRequest);
            }

            if (this.Db.Users.Any(x => x.Username == userName))
            {
                return new BadRequestResult("User with the same name already exists.", HttpResponseStatusCode.BadRequest);
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                return new BadRequestResult("Please provide password of length 6 or more.", HttpResponseStatusCode.BadRequest);
            }

            if (password != confirmPassword)
            {
                return new BadRequestResult(
                    "Passwords do not match.",
                    HttpResponseStatusCode.SeeOther);
            }

            var hashedPassword = this.hashService.Hash(password);

            var user = new User
            {
                Username = userName,
                Password = hashedPassword,
            };
            this.Db.Users.Add(user);
            this.Db.Users.Add(user);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception e)
            {
                return new BadRequestResult(
                    e.Message,
                    HttpResponseStatusCode.InternalServerError);
            }

            var response = new RedirectResult("/");
            this.SignInUser(userName, response, request);

            return response;
        }
        
        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie("auth-irunes"))
            {
                return new RedirectResult("/");
            }

            var cookie = request.Cookies.GetCookie("auth-irunes");
            cookie.Delete();
            var response = new RedirectResult("/");
            response.Cookies.Add(cookie);

            return response;
        }
    }
}
