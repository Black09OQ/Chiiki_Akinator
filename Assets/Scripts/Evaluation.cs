using System.Data;
using SQLite4Unity3d;

public class Evaluation
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int ProtocolID { get; set; }
    public int Value { get; set; }
}
