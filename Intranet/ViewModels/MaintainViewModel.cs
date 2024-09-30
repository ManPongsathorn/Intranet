using Intranet.Models;

namespace Intranet.ViewModels
{
    public class MaintainViewModel
    {
        public string? Remark { get; set; }
        public MenuGroup MenuGroup { get; set; }
        public UserMenuGroup UserMenuGroup { get; set; }
        public LocationGroup LocationGroup { get; set; }
        public Location Location { get; set; }
        public Department1 Department1 { get; set; }
        public Department2 Department2 { get; set; }
        public Section Section { get; set; }
        public Shop Shop { get; set; }
        public ShopGroup ShopGroup { get; set; }
        public ShopPositionGroup ShopPositionGroup { get; set; }
        public Position Position { get; set; }
        public ApplicationUser User { get; set; }
        public CategoryComplaint CategoryComplaint { get; set; }
        public ComplaintFrom ComplaintFrom { get; set; }
        public IEnumerable<MenuGroup> MenuGroups { get; set; }
        public IEnumerable<UserMenuGroup> UserMenuGroups { get; set; }
        public IEnumerable<LocationGroup> LocationGroups { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Department1> Department1s { get; set; }
        public IEnumerable<Department2> Department2s { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<ShopGroup> ShopGroups { get; set; }
        public IEnumerable<ShopPositionGroup> ShopPositionGroups { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<CategoryComplaint> CategoryComplaints { get; set; }
        public IEnumerable<ComplaintFrom> ComplaintFroms { get; set; }
    }
}
