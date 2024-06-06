using SQLite4Unity3d;
public class Result
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int QuestionId { get; set; }
    public string Answer { get; set; }
    public int UserId { get; set;}
}
