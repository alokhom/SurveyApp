using Cassandra.Mapping.Attributes;

namespace SurveyApp.Models
{
    [Table("store.users")]
    public class Books
    {
        public string userid  { get; set; }
        public string? item_count { get; set; }
        public string? last_update_timestamp { get; set; }
    }
}

private static IShipCrewDao dao;

protected IShipCrewDAO shipCrewDao
{
if (dao == null)
{
        dao = new ShipCrewDAO();
    }
return dao;
}