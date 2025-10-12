namespace FriendsTown.Domain;

public class Offer : Entity<Guid>
{
    private Offer(Guid id) : base(id) { }

    public Event Event { get; private set; }
    public string Description { get; private set; }

    private static void Validate(Event activityEvent, 
        string description)
    {
        if (activityEvent is null)
            throw new ArgumentException("Event is required");
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Name is required");
    }

    public void Update(Event activityEvent, string description)
    {
        Validate(activityEvent, description);

        Description = description;
        Event = activityEvent;
    }

    public static Offer Create(Guid id, Event theEvent,
        string description)
    {
        Validate(theEvent, description);
        Offer offer = new Offer(id)
        {
            Event = theEvent,
            Description = description   
        };

        return offer;
    }
}


