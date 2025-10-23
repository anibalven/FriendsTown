namespace FriendsTown.Data.Repositories
{
    public class ActivityRepository: IActivityRepository
    {
        private readonly FriendsTownContext _context;
        public ActivityRepository(FriendsTownContext context)
        {
            _context = context;
        }

        public void Add(Activity activity)
        {
            _context.Add(activity);
            _context.SaveChanges();
        }

        public Activity FindById(Guid id)
        {
            return _context.Activities.Find(id);
        }

        public void Delete(Guid id)
        {
            var activity = _context.Activities.Find(id);
            _context.Activities.Remove(activity);
            _context.SaveChanges();
        }

        public IEnumerable<Activity> GetAll()
        {
            return _context.Activities.OrderBy(a => a.Name);
        }

        public void Update(Activity activity)
        {
            var existing = _context.Activities.Find(activity.Id);
            if (existing is null)
                throw new KeyNotFoundException("Act. not found.");

            existing.Update(activity.Name, activity.Description); 

             _context.SaveChanges();
        }
    }
}

