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
    public class ApuestaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ApuestaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from 
                        db_prueba1.Apuesta
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
        public JsonResult Post(Apuesta apuesta)
        {
            string query = @"
                        insert into db_prueba1.Apuesta (ApuestaGolesSeleccion1, ApuestaGolesSeleccion2, ApuestaPartidoId) values
                                                    (@ApuestaGolesSeleccion1, @ApuestaGolesSeleccion2, @ApuestaPartidoId);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ApuestaGolesSeleccion1", apuesta.ApuestaGolesSeleccion1);
                    myCommand.Parameters.AddWithValue("@ApuestaGolesSeleccion2", apuesta.ApuestaGolesSeleccion2);
                    myCommand.Parameters.AddWithValue("@ApuestaPartidoId", apuesta.ApuestaPartidoId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Apuesta apuesta)
        {
            string query = @"
                        update db_prueba1.Apuesta set 
                        ApuestaGolesSeleccion1 =@ApuestaGolesSeleccion1,
                        ApuestaGolesSeleccion2 =@ApuestaGolesSeleccion2,
                        ApuestaPartidoId =@ApuestaPartidoId
                        where ApuestaId=@ApuestaId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ApuestaId", apuesta.ApuestaId);
                    myCommand.Parameters.AddWithValue("@ApuestaGolesSeleccion1", apuesta.ApuestaGolesSeleccion1);
                    myCommand.Parameters.AddWithValue("@ApuestaGolesSeleccion2", apuesta.ApuestaGolesSeleccion2);
                    myCommand.Parameters.AddWithValue("@ApuestaPartidoId", apuesta.ApuestaPartidoId);

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
                        delete from db_prueba1.Apuesta 
                        where ApuestaId=@ApuestaId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@ApuestaId", id);

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
