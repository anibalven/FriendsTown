using FriendsTown.Data.Repositories;
using FriendsTown.Domain;
using FriendsTown.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace FriendsTown.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository,
                              IConfiguration configuration)
        {
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            var noticias = _newsRepository.GetAll();
            var model = noticias.Select(a => new NewsViewModel
            {
                Description = a.Description,
                Date = a.Date.Value,
                City = a.Place.City,
                Street = a.Place.Street,           
                Number = a.Place.Number,
                Reference = a.Place.Reference
            });
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new NewsViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(NewsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var place = Place.Create(model.City, model.Street,
                    model.Number, model.Reference);
                var date = Date.FromDate(model.Date.Value);

                var news = News.Create(Guid.NewGuid(), date,
                    place, model.Description);

                _newsRepository.Add(news);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }

}

