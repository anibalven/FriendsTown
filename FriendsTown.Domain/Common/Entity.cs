public abstract class Entity<TId> : IEquatable<Entity<TId>>
{
    public TId Id { get; protected set; }
    protected Entity(TId id)
    {		
        this.Id = !EqualityComparer<TId>.Default.Equals(id, default) ? 
            id :  throw new ArgumentException(
                "You cannot use the default value for Id", "id");
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(this, obj)) return true;
		if (obj is Entity<TId> entity) return this.Equals(entity);

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<TId>.Default.GetHashCode(Id);
    }

    public bool Equals(Entity<TId> other)
    {
        return other is null ? false : EqualityComparer<TId>.Default
                                            .Equals(Id, other.Id);
    }
}

