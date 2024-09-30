using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class ComplaintFrom
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
    }
}
