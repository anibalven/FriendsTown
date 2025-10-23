namespace FriendsTown.Domain;

public class Friend : Entity<Guid>
{
    private Friend(Guid id) : base(id) { }

    public string Name { get; private set; }
    public string? Phone { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }

    private static void Validate(string name, string email,
        string password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password is required");
    }

    public static Friend Create(string name, string phone, string email,
        string password)
    {
        Validate(name, email, password);

        var friend = new Friend(Guid.NewGuid())
        {
            Name = name,
            Phone = phone,
            Email = email,
            Password = password
        };

        return friend;
    }

    public void Update(string name, string phone, string email, 
        string password)
    {
        Validate(name, email, password);

        Name = name;
        Phone = phone;
        Email = email;
        Password = password;
    }

    public bool HasPhone => !string.IsNullOrEmpty(Phone);
}

