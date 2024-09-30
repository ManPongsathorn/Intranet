using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class DirectoryGroup
    {
        public int Id { get; set; }
        public int MenuGroupId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Active { get; set; }
        [ForeignKey("MenuGroupId")]
        public MenuGroup MenuGroup { get; set; }
    }
}
