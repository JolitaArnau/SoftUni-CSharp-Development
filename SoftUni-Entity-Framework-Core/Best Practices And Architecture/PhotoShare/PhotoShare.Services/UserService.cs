namespace PhotoShare.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Models;
    using Data;

    public class UserService : IUserService
    {
        private readonly PhotoShareContext context;

        private IUserSessionService userSessionService;

        private ITownService townService;

        public UserService(PhotoShareContext context, IUserSessionService userSessionService, ITownService townService)
        {
            this.context = context;
            this.userSessionService = userSessionService;
            this.townService = townService;
        }


        public User ById(int id)
        {
            var user = this.context.Users.FirstOrDefault(u => u.Id.Equals(id));

            return user;
        }

        public User ByUsername(string username)
        {
            return this.context.Users
                .SingleOrDefault(u => u.Username == username);
        }

        public User ByUserNameAndPassword(string username, string password)
        {
            var user = this.context.Users.FirstOrDefault(
                u => u.Username.Equals(username) && u.Password.Equals(password));

            return user;
        }

        public User Login(string username, string password)
        {
            var user = this.ByUserNameAndPassword(username, password);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            this.userSessionService.User = user;

            user.LastTimeLoggedIn = DateTime.Now;

            context.SaveChanges();

            return user;
        }

        public string Logout()
        {
            var username = this.userSessionService.User.Username;
            this.userSessionService.User = null;

            return username;
        }


        public User Register(string username, string password, string repeatPassword, string email)
        {
            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            var user = this.ByUsername(username);

            if (user != null)
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

           user = new User()
            {
                Username = username,
                Password = password,
                Email = email,
                RegisteredOn = DateTime.Now
            };

            context.Users.Add(user);

            context.SaveChanges();

            return user;
        }

        public User ModifyUser(string username, string property, string newValue)
        {
            var user = this.ByUsername(this.userSessionService.User.Username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            switch (property)
            {
                case "Password":
                    if (!(newValue.Any(ch => Char.IsLower(ch)) && newValue.Any(ch => Char.IsDigit(ch))))
                    {
                        throw new ArgumentException($"Value {newValue} not valid. Invalid Password");
                    }

                    user.Password = newValue;
                    break;
                case "BornTown":
                    var town = this.townService.ByName(newValue);
                    if (town == null)
                    {
                        throw new ArgumentException($"Town {town.Name} not found!");
                    }

                    user.BornTown = town;
                    break;
                case "CurrentTown":
                    town = this.townService.ByName(newValue);
                    if (town == null)
                    {
                        throw new ArgumentException($"Value {newValue} not valid. Town {newValue} not found!");
                    }

                    user.CurrentTown = town;
                    break;
            }

            context.SaveChanges();

            return user;
        }

        public void Delete(string username)
        {
            var user = this.ByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if ((bool) user.IsDeleted)
            {
                throw new InvalidOperationException($"User {user.Username} is already deleted!");
            }

            user.IsDeleted = true;

            context.SaveChanges();
        }
    }
}