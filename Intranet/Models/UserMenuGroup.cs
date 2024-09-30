using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class UserMenuGroup
    {
        public int Id { get; set; }
        public int MenuGroupId { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
        [ForeignKey("MenuGroupId")]
        public MenuGroup MenuGroup { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
