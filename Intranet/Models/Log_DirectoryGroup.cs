namespace Intranet.Models
{
    public class Log_DirectoryGroup
    {
        public int Id { get; set; }
        public int DirectoryGroupId { get; set; }
        public int MenuGroupId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionOn { get; set; }
    }
}
