namespace Product_Shop.Models
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlType("category")]
    public class Category
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlElement("name")] 
        public string Name { get; set; }

        [XmlIgnore]
        public ICollection<CategoryProduct> CategoriesProducts { get; set; }
    }
}