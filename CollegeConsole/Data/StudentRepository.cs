using CollegeConsole.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CollegeConsole.Data;

public class StudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Student> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"
            SELECT s.Id, s.FirstName, s.LastName, s.Age, s.GroupId, g.Name AS GroupName
            FROM Students s
            LEFT JOIN Groups g ON s.GroupId = g.Id";

        return connection.Query<Student>(sql).ToList();
    }

    public void Add(Student student)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"
            INSERT INTO Students (FirstName, LastName, Age, GroupId) 
            VALUES (@FirstName, @LastName, @Age, @GroupId)";

        connection.Execute(sql, student);
    }

    public int CreateAndGetId(Student student)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"
            INSERT INTO Students (FirstName, LastName, Age, GroupId) 
            OUTPUT INSERTED.Id
            VALUES (@FirstName, @LastName, @Age, @GroupId)";

        return connection.QuerySingle<int>(sql, student);
    }
    public Student? FindByName(string name)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"
        SELECT s.Id, s.FirstName, s.LastName, s.Age, s.GroupId, g.Name AS GroupName
        FROM Students s
        LEFT JOIN Groups g ON s.GroupId = g.Id
        WHERE s.FirstName LIKE @Name OR s.LastName LIKE @Name";

        return connection.QueryFirstOrDefault<Student>(sql, new { Name = $"%{name}%" });
    }

    public List<Student> FindAllByName(string name)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"
        SELECT s.Id, s.FirstName, s.LastName, s.Age, s.GroupId, g.Name AS GroupName
        FROM Students s
        LEFT JOIN Groups g ON s.GroupId = g.Id
        WHERE s.FirstName LIKE @Name OR s.LastName LIKE @Name";

        return connection.Query<Student>(sql, new { Name = $"%{name}%" }).ToList();
    }

    public int CreateStudent(Student student)
    {
        using var connection = new SqlConnection(_connectionString);
        string sql = @"
        INSERT INTO Students (FirstName, LastName, Age, GroupId)
        OUTPUT INSERTED.Id
        VALUES (@FirstName, @LastName, @Age, @GroupId)";

        return connection.QuerySingle<int>(sql, student);
    }
}