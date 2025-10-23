using FriendsTown.Data.Repositories;
using FriendsTown.Models;
using FriendsTown.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace FriendsTown.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFriendRepository _friendRepository;

        public HomeController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }   

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("FriendName") == null)
            {
                return RedirectToAction("SelectFriend", "Home");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult SelectFriend()
        {
            var friends = _friendRepository.GetAll();

            var model = new SelectFriendViewModel
            {
                FriendNames = friends.Select(f => new SelectListItem
                {
                    Value = f.Name,
                    Text = f.Name
                }).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ProcessSelectFriend(string SelectedFriend)
        {
            HttpContext.Session.SetString("FriendName", SelectedFriend);
            return Redirect("~/");
        }

    }
}
