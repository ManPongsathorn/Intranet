using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Department1
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
    }
}
