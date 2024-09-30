using System.ComponentModel.DataAnnotations;

namespace Intranet.Models
{
    public class Log_ApplicationUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
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
        public string? PhoneNumber { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public string ActionByFirstName { get; set; }
        public string ActionByLastName { get; set; }
        public DateTime ActionOn { get; set; }
        public string? Remark { get; set; }
    }
}
