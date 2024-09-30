using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public bool Read { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
