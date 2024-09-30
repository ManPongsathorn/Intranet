namespace Intranet.Models
{
    public class Log_Like
    {
        public int Id { get; set; }
        public int LikeId { get; set; }
        public int PostId { get; set; }
        public bool CheckLike { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
    }
}
