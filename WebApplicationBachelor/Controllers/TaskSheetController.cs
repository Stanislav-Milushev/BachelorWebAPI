using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplicationBachelor.Models;

namespace WebApplicationBachelor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class TaskSheetController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TaskSheetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // get all tasksheets from a specific teacher, if id.Teacher=0 get all tasksheets
        
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                    select TaskSheetId, TaskSheetName, SubjectName, TeacherId from dbo.TaskSheet
                    where TeacherId = '" + id + @"'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TacherDashboardAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(TaskSheet task)
        {
            string query = @"
                    insert into dbo.TaskSheet values 
                    ('" + task.TaskSheetName + @"','" + task.SubjectName + @"',
                                    '" + task.TeacherId + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TacherDashboardAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table + "Added Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.TaskSheet
                    where TaskSheetId = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TacherDashboardAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
