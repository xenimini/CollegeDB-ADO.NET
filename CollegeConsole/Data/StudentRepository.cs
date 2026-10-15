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
}