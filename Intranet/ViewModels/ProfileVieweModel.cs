using Intranet.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Intranet.ViewModels
{
    public class ProfileVieweModel
    {
        public ApplicationUser User { get; set; }
        public UserRoleViewModel UserRoleViewModel { get; set; }
        public List<UserRoleViewModel> UserRoleViewModelsList { get; set; }
        public SelectList SelectRoles { get; set; }
        public IEnumerable<Department1> Department1s { get; set; }
        public IEnumerable<Department2> Department2s { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<Position> Positions { get; set; }
        public List<string>? AvatarImageProfile { get; set; }
        public List<string>? BgColor { get; set; }
    }
}
