using System.ComponentModel.DataAnnotations.Schema;

namespace Intranet.Models
{
    public class ShopGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
