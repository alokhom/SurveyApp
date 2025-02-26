using Cassandra;
using SurveyApp.DAO;

namespace SurveyApp.DAO
{
    public interface ICassandraDAO
    {
        Cassandra.ISession GetSession();
    }
}

public class CassandraDAO : ICassandraDAO
{
    private static Cluster? Cluster;
    private static Cassandra.ISession? Session;

    public CassandraDAO()
    {
        SetCluster();
    }

    private void SetCluster()
    {
        if (Cluster == null)
        {
            Cluster = Connect();
        }
    }

    public Cassandra.ISession GetSession()
    {
        if (Cluster == null)
        {
            SetCluster();
            Session = Cluster.Connect();
        }
        else if (Session == null)
        {
            Session = Cluster.Connect();
        }

        return Session;
    }

    private Cluster Connect()
    {
        string user = getAppSetting("UserName");
        string pwd = getAppSetting("Password");
        string[] nodes = getAppSetting("ContactPoints").Split(',');

        QueryOptions queryOptions = new QueryOptions().SetConsistencyLevel(ConsistencyLevel.One);


        Cluster cluster = Cluster.Builder()
        .AddContactPoints(nodes)
        .WithCredentials(user, pwd)
        .WithQueryOptions(queryOptions)
        .Build();

        return cluster;
    }

    private string getAppSetting(string key)
    {
        return System.Configuration.ConfigurationManager.AppSettings[key];
    }
}