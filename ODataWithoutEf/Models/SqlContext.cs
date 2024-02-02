using System.Data;
using System.Data.SqlClient;

namespace ODataWithoutEf.Models;

public class SqlContext
{
    private const string ConnectionString =
        "Server=localhost,1433;Database=ODataStudent;User=sa;Password=yourStrong(!)Password;TrustServerCertificate=True";

    public List<Student> Get()
    {
        var list = new List<Student>();
        var query = "Select * from Students";
        using var con = new SqlConnection(ConnectionString);
        using var cmd = new SqlCommand(query);
        cmd.Connection = con;
        var adp = new SqlDataAdapter(cmd);
        var dt = new DataTable();
        adp.Fill(dt);
        list.AddRange(from DataRow dr in dt.Rows
            select new Student { Id = Convert.ToInt32(dr[0]), FirstName = Convert.ToString(dr[1]), LastName = Convert.ToString(dr[2]) });

        return list;
    }

    public bool Add(Student obj)
    {
        var query = "insert into Students values('" + obj.FirstName + "','" + obj.LastName + "')";
        using var con = new SqlConnection(ConnectionString);
        using var cmd = new SqlCommand(query);
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
            con.Open();
        var i = cmd.ExecuteNonQuery();
        return i >= 1;
    }

    public bool Update(int id, Student obj)
    {
        var query = $"update Students set FirstName= '{obj.FirstName}', LastName='{obj.LastName}' where Id='{id}'";
        using var con = new SqlConnection(ConnectionString);
        using var cmd = new SqlCommand(query);
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
            con.Open();
        var i = cmd.ExecuteNonQuery();
        return i >= 1;
    }

    public bool Delete(int id)
    {
        var query = $"delete Students where Id='{id}'";
        using var con = new SqlConnection(ConnectionString);
        using var cmd = new SqlCommand(query);
        cmd.Connection = con;
        if (con.State == ConnectionState.Closed)
            con.Open();
        var i = cmd.ExecuteNonQuery();
        return i >= 1;
    }
}