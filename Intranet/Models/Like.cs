using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public bool CheckLike { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
