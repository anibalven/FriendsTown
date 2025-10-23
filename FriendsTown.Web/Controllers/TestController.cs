using FriendsTown.Transversal;
using FriendsTown.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        public ViewResult Welcome()
        {
            ViewBag.Framework = AppContext.TargetFrameworkName;
            ViewBag.Today = DateTime.Today;
            return View();
        }

        public ViewResult BodyMass(decimal? weight, decimal? height)
        {
            if (weight is not null && height is not null)
            {
                ViewBag.Index = weight.Value /
                    (height.Value * height.Value);
            }
            return View();
        }

        public ViewResult Doorman(int age)
        {
            ViewBag.Age = age;
            return View();
        }

        public ViewResult Multiply(int multiplier)
        {
            ViewBag.Multiplier = multiplier;
            return View();
        }

        public ViewResult Product()
        {
            var product = new ProductViewModel
            {
                Code = 2345,
                Name = "Bike",
                Price = 275
            };

            return View(product);
        }

        public ContentResult Sendmail([FromServices] IEmailService emailService)
        {
            emailService.SendMail(
                "Administrator@FriendsTown.com",
                "NewFriend@externalmail.com",
                "Welcome to FriendsTown",
                "<h3>Hi! NewFriend, FriendsTown welcomes you</h3>");
            return new ContentResult
            {
                Content = "Email sent"
            };
        }

        [HttpGet]
        public ViewResult MultipleProducts()
        {
            return View();
        }

        [HttpPost]
        public ContentResult MultipleProducts(string[] product)
        {
            return new ContentResult
            {
                Content = String.Join("-", product)
            };
        }

        [HttpGet]
        public ViewResult NextFibonacci(int number1 = 0,
            int number2 = 1)
        {
            @ViewBag.Number1 = number1;
            @ViewBag.Number2 = number2;
            @ViewBag.Next = number1 + number2;

            return View();
        }

        [HttpGet]
        public ViewResult NextFibonacciHidden()
        {
            FibonacciViewModel model = new FibonacciViewModel
            {
                Number1 = 1,
                Number2 = 1,
                Sequence = "0, 1"
            };

            return View(model);
        }

        [HttpPost]
        public ViewResult NextFibonacciHidden(FibonacciViewModel model)
        {
            int next = model.Number1 + model.Number2;

            model.Number1 = model.Number2;
            model.Number2 = next;
            model.Sequence += ", " + next;

            ModelState.Clear();
            return View(model);
        }

        [HttpGet]
        public ViewResult SetCookie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string cookieValue)
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(5),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };
            HttpContext.Response.Cookies.Append("MyCookie",
                              cookieValue, cookieOptions);

            return new ContentResult
            {
                Content = "<h4>Cookie Created <br><br> " +
                  $"<a href='{Url.ActionLink("GetCookie", "Test")}'>" +
                  "View cookie</a></h4>",
                ContentType = "text/html"
            };
        }

        [HttpGet]
        public IActionResult GetCookie()
        {
            var value = HttpContext.Request.Cookies["MyCookie"];

            return new ContentResult
            {
                Content = $"<h4>The cookie value is: {value}</h4>",
                ContentType = "text/html"
            };
        }

        [HttpGet]
        public ViewResult ViewOffice()
        {
            return View();
        }

        public IActionResult Chuck()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(
                "https://api.chucknorris.io/jokes/random").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var jsonData = JsonSerializer.Deserialize<ChuckViewModel>(data);
                return View(jsonData);
            }
            else
            {
                return NotFound();
            }
        }

        public ViewResult Cars()
        {
            return View();
        }
    }
}
