using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using SingerWebSiteIntegration.Models;

namespace Api_Integration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private DBConnection dbconnection = new DBConnection();
        [HttpGet("GetStudent")]
       public ActionResult GetStudentDtl([FromQuery] string Id)
{
    if (string.IsNullOrWhiteSpace(Id))
    {
        return BadRequest(new { status = "Error", message = "Parameter is Mandatory!" });
    }

    using (MySqlConnection conn = dbconnection.GetMysqlConnection())
    {
        conn.Open();
        using (MySqlCommand cmd = new MySqlCommand(@"SELECT Name, MobileNo FROM student WHERE Id = @Id", conn))
        {
            cmd.Parameters.AddWithValue("@Id", Id);

            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var result = new
                    {
                        Name = reader["Name"].ToString(),
                        MobileNumber = reader["MobileNo"].ToString(),
                    };
                    return Ok(new { status = "Success", data = result });
                }
                else
                {
                    return Ok(new { status = "Error", message = "Student Id cannot be found!" });
                }
            }
        }
    }
}

    }
}
