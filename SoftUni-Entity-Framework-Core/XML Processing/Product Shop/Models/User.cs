namespace Product_Shop.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("user")]
    public class User
    {
        public User()
        {
            this.SoldProducts = new List<Product>();

            this.BoughtProducts = new List<Product>();
        }
        
        [XmlIgnore]
        public int Id { get; set; }

        [XmlAttribute("firstName")]
        public string FirstName { get; set; }

        [XmlAttribute("lastName")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public int? Age { get; set; }

        [XmlIgnore]
        public ICollection<Product> SoldProducts { get; set; }

        [XmlIgnore]
        public ICollection<Product> BoughtProducts { get; set; }
    }
}