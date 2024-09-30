namespace Intranet.Models
{
    public class Log_Shop
    {
        public int Id { get; set; }
        public int ShopId { get; set; }
        public int SectionId { get; set; }
        public int ShopGroupId { get; set; }
        public int ShopPositionGroupId { get; set; }
        public string Branch { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public string Action { get; set; }
        public string ActionBy { get; set; }
        public string ActionByFirstName { get; set; }
        public string ActionByLastName { get; set; }
        public DateTime ActionOn { get; set; }
        public string? Remark { get; set; }
    }
}
