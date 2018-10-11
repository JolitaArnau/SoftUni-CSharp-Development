namespace IRunesWebApp.Models
{
    using System.Collections.Generic;

    public class User : BaseEntity<string>
    {
        public User()
        {
            this.Albums = new HashSet<Album>();
        }
        
        public string Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
