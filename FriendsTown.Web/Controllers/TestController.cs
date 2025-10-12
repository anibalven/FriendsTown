using Microsoft.AspNetCore.Mvc;

namespace FriendsTown.Web.Controllers
{
    public class TestController : Controller
    {
        public ContentResult Index()
        {
            string message = "Welcome to FriendsTown " +
               $"today is {DateTime.Today}";
            return new ContentResult
            {
                Content = message
            };
        }

        public IActionResult Html()
        {
            return new ContentResult
            {
                Content = "<h1>Welcome to FriendsTown</h1>",
                ContentType = "html"
            };
        }

        public ContentResult Forbidden()
        {
            return new ContentResult
            {
                StatusCode = 403
            };
        }

        public JsonResult JsonData()
        {
            var user = new { Id = 1, Name = "Mary" };
            return new JsonResult(user);

        }

        public ContentResult ShowCode(string id)
        {
            return new ContentResult
            {
                Content = $"Received Code: {id}"
            };
        }

        public ContentResult ProcessName(string name)
        {
            return new ContentResult
            {
                Content = $"Hi: {name} your name has " +
                    $"{name.Length} letters "
            };
        }

        public ContentResult NetPrice(decimal price, decimal discount)
        {
            decimal calculatedDiscount = price * discount / 100;
            decimal netPrice = price - calculatedDiscount;
            return new ContentResult
            {
                Content = $"Price: {price} discount: {calculatedDiscount} " +
                    $"Net Price: {netPrice}"
            };
        }
    }
}
