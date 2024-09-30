using Intranet.Models;

namespace Intranet.ViewModels
{
    public interface IBlogpostService
    {
        Task<UserMenuGroup> GetUserMenuGroupId(string id);
        Task<List<MenuGroup>> GetMenuGroupId(int id);
        Task<UserMenuGroup> UserMenuGroupId(string id);
    }
}
