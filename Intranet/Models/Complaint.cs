using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Complaint
    {
        public int Id { get; set; }
        public int CategoryComplaintId { get; set; }
        public string? Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("CategoryComplaintId")]
        public CategoryComplaint CategoryComplaint { get; set; }
    }
}
