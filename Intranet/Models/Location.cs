using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class Location
    {
        public int Id { get; set; }
        public int LocationGroupId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        [ForeignKey("LocationGroupId")]
        public LocationGroup LocationGroup { get; set; }
    }
}
