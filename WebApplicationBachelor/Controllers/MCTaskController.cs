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
    public class MCTaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public MCTaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        //specific get
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select MCTaskId, Rightanswer, Wronganswer1, Wronganswer2, Wronganswer3  from dbo.MCTask";
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
        public JsonResult Post(MCTask task)
        {
            string query = @"
                    insert into dbo.MCTask values 
                    ('" + task.Rightanswer + @"','" + task.Wronganswer1 + @"',
                                    '" + task.Wronganswer2 + @"','" + task.Wronganswer3 + @"')";
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
        [HttpPut]
        public JsonResult Put(MCTask task)
        {
            string query = @"update dbo.MCTask set Rightanswer='" + task.Rightanswer + @"'
                                    where MCTaskId=" + task.MCTaskId + @"
                                                           update dbo.MCTask set Wronganswer1='" + task.Wronganswer1 + @"'
                                    where MCTaskId=" + task.MCTaskId + @"
                                                           update dbo.MCTask set Wronganswer2='" + task.Wronganswer2 + @"'
                                    where MCTaskId=" + task.MCTaskId + @"
                                                           update dbo.MCTask set Wronganswer3='" + task.Wronganswer3 + @"'
                                    where MCTaskId=" + task.MCTaskId + @"
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

            return new JsonResult("Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.MCTask
                    where MCTaskId = " + id + @" 
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
