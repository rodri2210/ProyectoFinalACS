using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LigaUsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LigaUsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from 
                        db_prueba1.LigaUsuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand myCommand=new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(LigaUsuario ligausuario)
        {
            string query = @"
                        insert into db_prueba1.LigaUsuario (LigaUsuarioLigaId, LigaUsuarioUsuarioId) values
                                                    (@LigaUsuarioLigaId, @LigaUsuarioUsuarioId);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@LigaUsuarioLigaId", ligausuario.LigaUsuarioLigaId);
                    myCommand.Parameters.AddWithValue("@LigaUsuarioUsuarioId", ligausuario.LigaUsuarioUsuarioId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(LigaUsuario ligausuario)
        {
            string query = @"
                        update db_prueba1.LigaUsuario set 
                        LigaUsuarioLigaId =@LigaUsuarioLigaId,
                        LigaUsuarioUsuarioId =@LigaUsuarioUsuarioId
                        where LigaUsuarioId=@LigaUsuarioId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@LigaUsuarioId", ligausuario.LigaUsuarioId);
                    myCommand.Parameters.AddWithValue("@LigaUsuarioLigaId", ligausuario.LigaUsuarioLigaId);
                    myCommand.Parameters.AddWithValue("@LigaUsuarioUsuarioId", ligausuario.LigaUsuarioUsuarioId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }



        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                        delete from db_prueba1.LigaUsuario 
                        where LigaUsuarioId=@LigaUsuarioId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@LigaUsuarioId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
