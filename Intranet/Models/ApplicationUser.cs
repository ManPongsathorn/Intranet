using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? EmployeeId { get; set; }
        public string FirstNameEN { get; set; }
        public string LastNameEN { get; set; }
        public string FirstNameTH { get; set; }
        public string LastNameTH { get; set; }
        public string? NickName { get; set; }
        public string? ImageUser { get; set; }
        public string? BgUser { get; set; }
        public int? ShopId { get; set; }
        public int? PositionId { get; set; }
        public string? InternalPhoneNumber { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public bool Active { get; set; }
        [ForeignKey("ShopId")]
        public Shop? Shop { get; set; }
        [ForeignKey("PositionId")]
        public Position? Position { get; set; }
    }
}
