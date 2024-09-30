using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Section
    {
        public int Id { get; set; }
        public int Department1Id { get; set; }
        public int? Department2Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [ForeignKey("Department1Id")]
        public Department1 Department1 { get; set; }
        [ForeignKey("Department2Id")]
        public Department2? Department2 { get; set; }

    }
}
