using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string UserID { get; set;}
    public string UserName { get; set; }
    public string Password { get; set; }

}
