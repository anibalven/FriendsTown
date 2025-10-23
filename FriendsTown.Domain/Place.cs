namespace FriendsTown.Domain;

public class Place : ValueObject
{
    private Place() { }

    public required string City { get; init; }
    public required string Street { get; init; }
    public required string Number { get; init; }
    public string Reference { get; init; }

    public static Place Create(string city, string street,
        string number, string reference)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required", 
                nameof(city));
        if (string.IsNullOrWhiteSpace(street))
            throw new ArgumentException("Street is required", 
                nameof(street));
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Number is required", 
                nameof(number));
        Place place = new Place
        {
            City = city,
            Street = street,
            Number = number,
            Reference = reference
        };
        return place;
    }

    public override string ToString() =>
        $"City: {City}, Street: {Street}, Number: {Number} " +
        $"{(Reference != null ? $"Reference: {Reference}" : "")}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return Number;
        yield return Reference;
    }
}

