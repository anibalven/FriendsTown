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
        private readonly ILogger<ActivityController> _logger;
        private string CacheKey => "ActivitiesCacheKey";

        public ActivityController(IActivityRepository activityRepository,
                                  ICacheService cacheService,
                                  ILogger<ActivityController> logger)
        {
            _activityRepository = activityRepository;
            _cacheService = cacheService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Starting call to Activity/Index method");
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

            _logger.LogInformation("Call to Activity/Index method completed");

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
            _logger.LogInformation("Starting Call to Activity/Details " +
                 $"method with ActivityId .{id}");

            var activity = _activityRepository.FindById(id);
            var model = new ActivityViewModel
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description
            };

            _logger.LogInformation("Call to Activity/Details " +
                 $" completed. Name: {model.Name}");

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

