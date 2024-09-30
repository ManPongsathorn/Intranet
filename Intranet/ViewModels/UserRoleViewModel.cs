using Intranet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intranet.ViewModels
{
    public class UserRoleViewModel
    {
        public string Id { get; set; }
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
        public string NewRole { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public Department1 Department1 { get; set; }
        public Department2 Department2 { get; set; }
        public Section Section { get; set; }
        public Shop? Shop { get; set; }
        public Position? Position { get; set; }

    }
}
