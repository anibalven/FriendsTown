namespace FriendsTown.Data.Repositories;

public class NewsRepository : INewsRepository
{
    private readonly FriendsTownContext _contexto;
    public NewsRepository(FriendsTownContext contexto)
    {
        _contexto = contexto;
    }

    public void Add(News news)
    {
        _contexto.Add(news);
        _contexto.SaveChanges();
    }

    public News FindById(Guid id)
    {
        return _contexto.News.Find(id);
    }

    public IEnumerable<News> GetAll()
    {
        return _contexto.News.OrderBy(a => a.Date.Value);
    }       
}


 