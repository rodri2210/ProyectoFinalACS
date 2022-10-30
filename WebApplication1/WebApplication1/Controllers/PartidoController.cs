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
    public class PartidoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PartidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from 
                        db_prueba1.Partido
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
        public JsonResult Post(Partido partido)
        {
            string query = @"
                        insert into db_prueba1.Partido (PartidoFecha, PartidoGolesSeleccion1, PartidoGolesSeleccion2, PartidoSedesId) values
                                                    (@PartidoFecha, @PartidoGolesSeleccion1, @PartidoGolesSeleccion2, @PartidoSedesId);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoFecha", partido.PartidoFecha);
                    myCommand.Parameters.AddWithValue("@PartidoGolesSeleccion1", partido.PartidoGolesSeleccion1);
                    myCommand.Parameters.AddWithValue("@PartidoGolesSeleccion2", partido.PartidoGolesSeleccion2);
                    myCommand.Parameters.AddWithValue("@PartidoSedesId", partido.PartidoSedesId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Partido partido)
        {
            string query = @"
                        update db_prueba1.Partido set 
                        PartidoFecha =@PartidoFecha,
                        PartidoGolesSeleccion1 =@PartidoGolesSeleccion1,
                        PartidoGolesSeleccion2 =@PartidoGolesSeleccion2,
                        PartidoSedesId =@PartidoSedesId
                        where PartidoId=@PartidoId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoId", partido.PartidoId);
                    myCommand.Parameters.AddWithValue("@PartidoFecha", partido.PartidoFecha);
                    myCommand.Parameters.AddWithValue("@PartidoGolesSeleccion1", partido.PartidoGolesSeleccion1);
                    myCommand.Parameters.AddWithValue("@PartidoGolesSeleccion2", partido.PartidoGolesSeleccion2);
                    myCommand.Parameters.AddWithValue("@PartidoSedesId", partido.PartidoSedesId);

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
                        delete from db_prueba1.Partido 
                        where PartidoId=@PartidoId;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoId", id);

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
