namespace FriendsTown.Domain;

public class Date : ValueObject
{
    private Date() { }

    private Date(DateTime date)
    {
        if (date.CompareTo(DateTime.Now) <= 0)
        {
            throw new ArgumentException
                ("Date must be greater than today");
        }
        Value = date;
    }

    public DateTime Value { get; init; }

    public static Date FromString(string stringDate)
    {
        if (DateTime.TryParse(stringDate, out var converted))
        {
            return new Date(converted);
        }
        else
        {
            throw new ArgumentException("Incorrect date");
        }
    }

    public static Date FromDate(DateTime date) => new Date(date);

    public int RemainingDays => (Value - DateTime.Now).Days;

    public string Announcement => $"We wait for you on " +
        $" {Value.ToShortDateString()}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

