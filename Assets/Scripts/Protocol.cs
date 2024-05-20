using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

public class Protocol
{   
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public int WorkID { get; set; }
    public int Order { get; set; }
    public string Name { get; set; }
}
