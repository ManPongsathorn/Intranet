using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Position
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        public int? ShopPositionGroupId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [ForeignKey("SectionId")]
        public Section Section { get; set; }
        [ForeignKey("ShopPositionGroupId")]
        public ShopPositionGroup? ShopPositionGroup { get; set; }
    }
}
