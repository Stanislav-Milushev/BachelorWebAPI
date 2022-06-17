using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApplicationBachelor.Models;
using System.Configuration;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;



namespace WebApplicationBachelor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TeacherController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        //login mit default email und pw, wenn Teacherid=0 bekommt man alle teacher in der db, wenn Teacherid=1
        public JsonResult Get()
        {
            string query = @"
                    select TeacherId, Firstname, Lastname, Email, Password  from dbo.Teacher";
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
        // create new Teacher 
        public JsonResult Post(Teacher newteacher)
        {
            string query = @"
                    insert into dbo.Teacher values 
                    ('" + newteacher.Firstname + @"','" + newteacher.Lastname + @"',
                                    '" + newteacher.Email + @"','" + newteacher.Password + @"')";
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

    }

}
