using FriendsTown.Data.Repositories;
using FriendsTown.Domain;
using FriendsTown.Transversal;
using FriendsTown.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FriendsTown.Web.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICacheService _cacheService;
        private string CacheKey => "ActivitiesCacheKey";

        public ActivityController(IActivityRepository activityRepository,
                                  ICacheService cacheService)
        {
            _activityRepository = activityRepository;
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            var activities = _cacheService.Get<IEnumerable<Activity>>(CacheKey);

            if (activities == null)
            {
                activities = _activityRepository.GetAll();
                _cacheService.Set(CacheKey, activities);
            }

            var model = activities.Select(a => new ActivityViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var activity = _activityRepository.FindById(id);
            var model = new ActivityViewModel
            {
                Id = id,
                Name = activity.Name,
                Description = activity.Description
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(ActivityViewModel model)
        {
            var actividad = Activity.Create(model.Id,
                model.Name, model.Description);
            _activityRepository.Update(actividad);
            return RedirectToAction("Index");
        }


        public IActionResult Details(Guid id)
        {
            var activity = _activityRepository.FindById(id);
            var model = new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new ActivityViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ActivityViewModel model)
        {
            var activity = Activity.Create(Guid.NewGuid(),
                model.Name, model.Description);
            _activityRepository.Add(activity);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var actividad = _activityRepository.FindById(id);
            var model = new ActivityViewModel
            {
                Id = actividad.Id,
                Name = actividad.Name,
                Description = actividad.Description
            };
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _activityRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

