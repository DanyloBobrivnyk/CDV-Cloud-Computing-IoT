using SQLite;
namespace IoT_Project_Food_Ordering.Models
{
    public interface ISqLite
    {
        SQLiteConnection GetConnection();
    }
}
