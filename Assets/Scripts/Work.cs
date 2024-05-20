using System;
using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;

[Serializable]
public class Work
{
    [PrimaryKey, AutoIncrement]
    public int ID {get; set;}
    public string Name {get; set;}
}
