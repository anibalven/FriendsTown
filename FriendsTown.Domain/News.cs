namespace FriendsTown.Domain
{
    public class News : Entity<Guid>
    {
        private News(Guid id) : base(id) { }

        public Date Date { get; protected set; }
        public Place Place { get; protected set; }
        public string Description { get; protected set; }

        private static void Validate(Date date, Place place, 
            string description)
        {
            if (date is null)
                throw new ArgumentException("Date is required");
            if (place is null)
                throw new ArgumentException("Place is required");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required");
        }

        public void Update(Date date, Place place, string description)
        {
            Validate(date, place, description);

            Description = description;
            Date = date;
            Place = place;
        }

        public static News Create(Guid id, Date date,  Place place, 
            string description)
        {
            Validate(date, place, description);

            News news = new News(id)
            {
                Date = date,
                Place = place,
                Description = description   
            };
            return news;
        }
    }
}

