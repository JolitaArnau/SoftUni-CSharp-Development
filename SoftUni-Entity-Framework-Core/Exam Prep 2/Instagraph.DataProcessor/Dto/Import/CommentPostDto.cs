using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dto.Import
{
    public class CommentPostDto
    {
        [Required]
        [XmlAttribute("id")]
        public string Id { get; set; }
    }
}