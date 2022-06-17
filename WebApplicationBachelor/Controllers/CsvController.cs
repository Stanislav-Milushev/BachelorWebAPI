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
    public class CsvController : Controller
    {
        private readonly IConfiguration _configuration;
        public CsvController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{TaskSheetId}/{TaskTypeId}")]
        public JsonResult Get(int TaskSheetId, int TaskTypeId)
        {
            string query = @"";
            if(TaskTypeId == 1)
            {
                query = @"
                    Select TaskCollection.Question,MCTask.Rightanswer,MCTask.Wronganswer1,MCTask.Wronganswer2,MCTask.Wronganswer3 from dbo.TaskInSheet 
                    INNER JOIN dbo.TaskCollection ON (TaskInSheet.TaskId = TaskCollection.TaskId)
                    INNER JOIN dbo.MCTask ON (TaskCollection.SpecificTaskId = MCTask.MCTaskId)
                    WHERE TaskInSheet.TaskSheetId = '" + TaskSheetId + @"'
                    AND TaskTypeId = 1";
            }else if(TaskTypeId == 2)
            {
                query = @"
                    Select TaskCollection.Question,FTTask.Answer,FTTask.TaskTip from dbo.TaskInSheet 
                    INNER JOIN dbo.TaskCollection ON (TaskInSheet.TaskId = TaskCollection.TaskId)
                    INNER JOIN dbo.FTTask ON (TaskCollection.SpecificTaskId = FTTask.FTTaskId)
                    WHERE TaskInSheet.TaskSheetId = '" + TaskSheetId + @"'
                    AND TaskTypeId = 2";
            }
            
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
        [HttpGet("{TaskSheetid}/{TaskTypeId}/{TaskId}")]
        public JsonResult Get(int TaskSheetid, int TaskTypeId, int TaskId)
        {
            string query = @"";
            if (TaskTypeId == 1)
            {
                query = @"
                    Select TaskCollection.Question,MCTask.Rightanswer,MCTask.Wronganswer1,MCTask.Wronganswer2,MCTask.Wronganswer3,MCTask.MCTaskId from dbo.TaskInSheet 
                    INNER JOIN dbo.TaskCollection ON (TaskInSheet.TaskId = TaskCollection.TaskId)
                    INNER JOIN dbo.MCTask ON (TaskCollection.SpecificTaskId = MCTask.MCTaskId)
                    WHERE TaskInSheet.TaskSheetId = '" + TaskSheetid + @"'
                    AND TaskTypeId = 1
                    AND TaskCollection.TaskId= '" + TaskId + @"'";
            }
            else if (TaskTypeId == 2)
            {
                query = @"
                    Select TaskCollection.Question,FTTask.Answer,FTTask.TaskTip,FTTask.FTTaskId from dbo.TaskInSheet 
                    INNER JOIN dbo.TaskCollection ON (TaskInSheet.TaskId = TaskCollection.TaskId)
                    INNER JOIN dbo.FTTask ON (TaskCollection.SpecificTaskId = FTTask.FTTaskId)
                    WHERE TaskInSheet.TaskSheetId = '" + TaskSheetid + @"'
                    AND TaskTypeId = 2
                    AND TaskCollection.TaskId= '" + TaskId + @"';";
            }

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
    }
}
