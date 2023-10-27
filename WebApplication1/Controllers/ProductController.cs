using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ProductController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select ID, PRODUTO_PUBLI, PRODUTO_FINAL,DIVISOR,
                    convert(varchar(10),DT_INICIAL,120) as DT_INICIAL,
                    convert(varchar(10),DT_FINAL,120) as DT_FINAL,GRUPO,
                    PERCENTUAL_RATEIO,CCUSTO_CLI,ORDEM_INTERNA
                    from 
                    dbo.ims_investimento_produto_dev
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }

        
        [HttpPost]
        public JsonResult Post(Product prod)
        {
            string query = @"
                    insert into dbo.ims_investimento_produto_dev
                    (PRODUTO_PUBLI,PRODUTO_FINAL,DIVISOR,DT_INICIAL,DT_FINAL,GRUPO,PERCENTUAL_RATEIO,CCUSTO_CLI,ORDEM_INTERNA)
                    values (@PRODUTO_PUBLI,@PRODUTO_FINAL,@DIVISOR,@DT_INICIAL,@DT_FINAL,@GRUPO,@PERCENTUAL_RATEIO,@CCUSTO_CLI,@ORDEM_INTERNA)
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@PRODUTO_PUBLI", prod.PRODUTO_PUBLI);
                    myCommand.Parameters.AddWithValue("@PRODUTO_FINAL", prod.PRODUTO_FINAL);
                    myCommand.Parameters.AddWithValue("@DIVISOR", prod.DIVISOR);
                    myCommand.Parameters.AddWithValue("@DT_INICIAL", prod.DT_INICIAL);
                    myCommand.Parameters.AddWithValue("@DT_FINAL", prod.DT_FINAL);
                    myCommand.Parameters.AddWithValue("@GRUPO", prod.GRUPO);
                    myCommand.Parameters.AddWithValue("@PERCENTUAL_RATEIO", prod.PERCENTUAL_RATEIO ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@CCUSTO_CLI", prod.CCUSTO_CLI);
                    myCommand.Parameters.AddWithValue("@ORDEM_INTERNA", prod.ORDEM_INTERNA);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Product prod)
        {
            string query = @"
                    update dbo.ims_investimento_produto_dev 
                    set PRODUTO_PUBLI=@PRODUTO_PUBLI,
                    PRODUTO_FINAL=@PRODUTO_FINAL,
                    DIVISOR=@DIVISOR,
                    DT_INICIAL=@DT_INICIAL,
                    DT_FINAL=@DT_FINAL,
                    GRUPO=@GRUPO,
                    PERCENTUAL_RATEIO=@PERCENTUAL_RATEIO,
                    CCUSTO_CLI=@CCUSTO_CLI,
                    ORDEM_INTERNA=@ORDEM_INTERNA
                    where ID=@ID
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", prod.ID);
                    myCommand.Parameters.AddWithValue("@PRODUTO_PUBLI", prod.PRODUTO_PUBLI);
                    myCommand.Parameters.AddWithValue("@PRODUTO_FINAL", prod.PRODUTO_FINAL);
                    myCommand.Parameters.AddWithValue("@DIVISOR", prod.DIVISOR);
                    myCommand.Parameters.AddWithValue("@DT_INICIAL", prod.DT_INICIAL);
                    myCommand.Parameters.AddWithValue("@DT_FINAL", prod.DT_FINAL);
                    myCommand.Parameters.AddWithValue("@GRUPO", prod.GRUPO);
                    myCommand.Parameters.AddWithValue("@PERCENTUAL_RATEIO", prod.PERCENTUAL_RATEIO ?? (object)DBNull.Value);
                    myCommand.Parameters.AddWithValue("@CCUSTO_CLI", prod.CCUSTO_CLI);
                    myCommand.Parameters.AddWithValue("@ORDEM_INTERNA", prod.ORDEM_INTERNA);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{ID}")]
        public JsonResult Delete(int ID)
        {
            string query = @"
                    delete from dbo.ims_investimento_produto_dev 
                    where ID=@ID
                    ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", ID);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
