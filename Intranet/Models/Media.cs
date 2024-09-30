using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Media
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public long Size { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
