using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dto.Export
{
    public class UserExportDto
    {
        [Required] 
        [XmlElement("Username")] 
        public string Username { get; set; }

        [Required]
        [XmlElement("MostComments")]
        public int MostComments { get; set; }

    }
}