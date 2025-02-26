using Cassandra.Mapping;
using SurveyApp.DAO;

namespace SurveyApp.DAO
{
    public interface BooksDAO
    {
        Task> getCrew();
    }
}

public class BookDAO : BooksDAO
{
    protected readonly ISession session;
    protected readonly IMapper mapper;

    public BookDAO()
    {
        ICassandraDAO cassandraDAO = new CassandraDAO();
        session = cassandraDAO.GetSession();
        mapper = new Mapper(session);
    }

    public async Task> getCrew()
    {
        return await mapper.FetchAsync();
    }
}
