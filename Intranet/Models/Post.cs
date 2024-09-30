using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int MenuGroupId { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("MenuGroupId")]
        public MenuGroup MenuGroup { get; set; }
    }
}
