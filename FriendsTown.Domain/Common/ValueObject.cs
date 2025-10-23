namespace FriendsTown.Domain.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public static bool operator ==(ValueObject? first, ValueObject? second)
    {
        if (first is null && second is null) return true;

        if (first is null || second is null) return false;

        return first.Equals(second);
    }

    public static bool operator !=(ValueObject? first, ValueObject? second)
        => !(first == second);

    public virtual bool Equals(ValueObject? other) =>
        other is not null && ValuesAreEqual(other);

    public override bool Equals(object? obj) =>
        obj is ValueObject valueObject && ValuesAreEqual(valueObject);

    public override int GetHashCode() =>
        GetEqualityComponents().Aggregate(default(int),
            (hashcode, value) =>
                HashCode.Combine(hashcode, value.GetHashCode()));

    protected abstract IEnumerable<object> GetEqualityComponents();

    private bool ValuesAreEqual(ValueObject valueObject) =>
        GetEqualityComponents().SequenceEqual(valueObject
            .GetEqualityComponents());
}
