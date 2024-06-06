using System.Data.Common;
using SQLite4Unity3d;

public class MoviePath
{
    [PrimaryKey, AutoIncrement]
    public static int ID { get; set; }
    public string Path { get; set; }
    public int UserID { get; set; }
    public int WorkID { get; set; }
}
