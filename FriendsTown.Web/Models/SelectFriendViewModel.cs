using Microsoft.AspNetCore.Mvc.Rendering;

namespace FriendsTown.Web.Models
{
    public class SelectFriendViewModel
    {
        public List<SelectListItem> FriendNames { get; set; }
        public string SelectedFriend { get; set; }
    }
}
