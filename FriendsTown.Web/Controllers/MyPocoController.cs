using Microsoft.AspNetCore.Mvc;

namespace FriendsTown.Web.Controllers;

public class MyPocoController 
{
    public IActionResult Index()
    {
        return new ContentResult { 
                Content = "This is a Poco Controller!" 
            };
    }
}

