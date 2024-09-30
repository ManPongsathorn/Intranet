namespace Intranet.Models
{
    public class HubConnection
    {
        public int Id { get; set; }
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public DateTime ConnectionOn { get; set; }
    }
}
