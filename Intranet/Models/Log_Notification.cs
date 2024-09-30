namespace Intranet.Models
{
    public class Log_Notification
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int PostId { get; set; }
        public bool Read { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
    }
}
