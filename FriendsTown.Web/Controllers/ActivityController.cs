using FriendsTown.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendsTown.Web.Controllers;

public class ActivityController : Controller
{
    public IActionResult Index()
    {
        var model = new List<ActivityViewModel>
        {
            new ActivityViewModel {Id=Guid.NewGuid(), Name="Climbing",
            Description="Climb local peaks"},
            new ActivityViewModel {Id=Guid.NewGuid(), Name="Hiking",
            Description="walk through natural trails"},
            new ActivityViewModel {Id=Guid.NewGuid(), Name="Yoga",
            Description="Activity in a park"},
        };
        return View(model);
    }
}

