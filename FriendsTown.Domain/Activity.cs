namespace FriendsTown.Domain;

public class Activity : Entity<Guid>
{
    public Activity(Guid id) : base(id) { }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    private static void Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");
    }

    public void Update(string name, string description)
    {
        Validate(name);
        Name =  name;
        Description = description;
    }

    public static Activity Create(Guid id, string name,
        string description)
    {
        Validate(name);
        Activity activity = new Activity(id)
        {
            Name = name,
            Description = description
        };

        return activity;
    }
}

