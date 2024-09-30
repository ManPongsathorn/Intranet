namespace Intranet.Models
{
    public class Log_UserMenuGroup
    {
        public int Id { get; set; }
        public int UserMenuGroupId { get; set; }
        public int MenuGroupId { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public string ActionByFirstName { get; set; }
        public string ActionByLastName { get; set; }
        public DateTime ActionOn { get; set; }
        public string? Remark { get; set; }
    }
}
