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
    public class TaskCollectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public TaskCollectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                    select TaskId, TaskTypeId, SpecificTaskId, Question from dbo.TaskCollection;";
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
        [HttpGet]
        public JsonResult Get(TaskCollection tc)
        {
            string query = @"
                    select TaskId from dbo.TaskCollection
                    where TaskTypeId='" + tc.TaskTypeId + @"'
                    and SpecificTaskId='" + tc.SpecificTaskId + @"'
                    and Question='" + tc.Question + @"'";
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
        public JsonResult Post(TaskCollection taskcol)
        {
            string query = @"
                    insert into dbo.TaskCollection values 
                    ('" + taskcol.TaskTypeId + @"',
                                    '" + taskcol.SpecificTaskId + @"','" + taskcol.Question + @"')";

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
        public JsonResult Put(TaskCollection taskcol)
        {
            string query = @"update dbo.TaskCollection set Question='" + taskcol.Question + @"'
                                    where TaskId=" + taskcol.TaskId + @"
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
    }
}
