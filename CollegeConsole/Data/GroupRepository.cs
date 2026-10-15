using CollegeConsole.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CollegeConsole.Data;

public class GroupRepository
{
    private readonly string _connectionString;

    public GroupRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<Group> GetAll()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Group>("SELECT Id, Name FROM Groups").ToList();
    }

    public void Add(string name)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("INSERT INTO Groups (Name) VALUES (@Name)", new { Name = name });
    }
}