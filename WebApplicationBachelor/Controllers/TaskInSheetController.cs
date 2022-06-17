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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationBachelor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

   
    public class TaskInSheetController : ControllerBase
    { 
        private readonly IConfiguration _configuration;
        public TaskInSheetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET api/<ValuesController>/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public JsonResult get(int id)
        {
            string query = @"
                    select TaskId from dbo.TaskInSheet
                    Where TaskSheetId='" + id + @"'";
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
        

        // POST api/<ValuesController>
        [HttpPost]
        public JsonResult Post(TaskInSheet taskinsheet)
        {
            string query = @"
                    USE TeacherData; 
                    ALTER TABLE TaskInSheet  
                    NOCHECK CONSTRAINT TIS_TS_TaskSheet_Id;  

                    insert into dbo.TaskInSheet values  
                    ('" + taskinsheet.TaskSheetId + @"','" + taskinsheet.TaskId + @"') 

                    USE TeacherData;
                    ALTER TABLE TaskInSheet
                    CHECK CONSTRAINT TIS_TS_TaskSheet_Id;
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

            return new JsonResult(table + "Added Successfully");
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        public JsonResult Put(TaskInSheet taskinsheet)
        {
            string query = @"
                    USE TeacherData; 
                    ALTER TABLE TaskInSheet  
                    NOCHECK CONSTRAINT TIS_TS_TaskSheet_Id;  

                    insert into dbo.TaskInSheet values  
                    ('" + taskinsheet.TaskSheetId + @"','" + taskinsheet.TaskId + @"') 

                    USE TeacherData;
                    ALTER TABLE TaskInSheet
                    CHECK CONSTRAINT TIS_TS_TaskSheet_Id;
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

            return new JsonResult(table + "Added Successfully");
        }

        // DELETE api/<ValuesController>/5 
        [HttpDelete]
        public JsonResult Delete(TaskInSheet taskInSheet)
        {
            string query = @"
                    delete from dbo.TaskInSheet
                    where TaskSheetId = '" + taskInSheet.TaskSheetId + @"'
                    and TaskId = '" + taskInSheet.TaskId + @"'
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
