using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dto.Import
{
    [XmlType("comment")]
    public class CommentDto
    {
        [Required] 
        [XmlElement("content")] 
        public string Content { get; set; }

        [XmlElement("user")] 
        public string User { get; set; }

        [Required]
        [XmlElement("post")]
        public CommentPostDto PostId { get; set; }
    }
}