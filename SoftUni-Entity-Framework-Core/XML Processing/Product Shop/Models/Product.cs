namespace Product_Shop.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Product
    {
        
        [XmlIgnore]
        public int Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlIgnore]
        public int? BuyerId { get; set; }

        [XmlIgnore]
        public User Buyer { get; set; }

        [XmlIgnore]
        public int SellerId { get; set; }

        [XmlIgnore]
        public User Seller { get; set; }

        [XmlIgnore]
        public ICollection<CategoryProduct> CategoriesProducts { get; set; }
    }
}