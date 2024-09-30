using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
