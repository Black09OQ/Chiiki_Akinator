using System.Data;
using SQLite4Unity3d;

public class Question
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string Name { get; set; }
    public int WorkID { get; set; }
    public int ProtocolID { get; set; }
}
