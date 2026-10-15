namespace CollegeConsole.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public int? GroupId { get; set; }
    public string? GroupName { get; set; }

    public override string ToString()
    {
        string group = string.IsNullOrEmpty(GroupName) ? "без группы" : GroupName;
        return $"{Id} | {FirstName} {LastName} | Возраст: {Age} | Группа: {group}";
    }
}