namespace FriendsTown.Domain;

public class Event : Entity<Guid>
{
    public Event(Guid id) : base(id) { }

    public Friend Organizer { get; protected set; }
    public Activity ActivityType { get; protected set; }
    public Date Date { get; protected set; }
    public Place Place { get; protected set; }
    public List<Friend> Participants { get; protected set; } = new();
    public List<Offer> Offers { get; protected set; } = new ();

    private void Validate(Friend organizer, Activity activity,
        Date date, Place place)
    {
        if (organizer is null)
            throw new ArgumentException("Organizer is required");
        if (activity is null)
            throw new ArgumentException("Activity is required");
        if (date is null)
            throw new ArgumentException("Date is required");
        if (place is null)
            throw new ArgumentException("Place is required");
    }
    public void Update(Friend organizer, Activity activity,
        Date date, Place place)
    {
        Validate(organizer, activity, date, place);

        Organizer = organizer;
        ActivityType = activity;
        Date = date;
        Place = place;
    }

    public static Event Create(Guid id, Friend organizer,
      Activity activity, Date date, Place place, 
      List<string> offers)
    {
        Event activityEvent = new Event(id)
        {
            Organizer = organizer,
            ActivityType = activity,
            Date = date,
            Place = place,
        };

        activityEvent.Offers = activityEvent.Offers = offers.Select(o =>
                Offer.Create(Guid.NewGuid(),
                activityEvent, o)).ToList();

        return activityEvent;
    }


    public void AddParticipant(Friend participant)
    {
        Participants.Add(participant);
    }

    public void RemoveParticipant(Friend participant)
    {
        if (Participants.Contains(participant))
        {
            Participants.Remove(participant);
        }
    }
}

