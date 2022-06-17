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
    public class FTTaskController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FTTaskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            
            string  query = @" select FTTaskId, Answer, TaskTip from dbo.FTTask";
            
                
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
        public JsonResult Post(FTTask task)
        {
            string query = @"
                    insert into dbo.FTTask values 
                    ('" + task.Answer + @"',
                                    '" + task.TaskTip + @"')";
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

            return new JsonResult(table + "Feetext Task Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(FTTask task)
        {
            string query = @"update dbo.FTTask set Answer='" + task.Answer + @"'
                                    where FTTaskId=" + task.FTTaskId + @"
                                                           update dbo.FTTask set TaskTip='" + task.TaskTip + @"'
                                    where FTTaskId=" + task.FTTaskId + @"";
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

            return new JsonResult("Feetext Task Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.FTTask
                    where FTTaskId = " + id + @" 
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

            return new JsonResult("Feetext Task Deleted Successfully");
        }
    }
}
