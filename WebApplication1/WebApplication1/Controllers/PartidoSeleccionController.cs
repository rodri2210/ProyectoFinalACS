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
    public class PartidoSeleccionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PartidoSeleccionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /*
         select * from 
                        db_prueba1.PartidoSeleccion
         */

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        { 
            string query = @"
                        Select db_prueba1.PartidoSeleccion.PartidoSeleccionID, db_prueba1.partido.PartidoId, db_prueba1.partido.PartidoFecha, 
                        db_prueba1.seleccion.SeleccionNombre, db_prueba1.sede.SedeNombre, db_prueba1.partido.PartidoGolesSeleccion1, db_prueba1.partido.PartidoGolesSeleccion2
                        From (((db_prueba1.PartidoSeleccion
                        INNER JOIN db_prueba1.seleccion ON db_prueba1.PartidoSeleccion.PartidoSeleccionSeleccionId = db_prueba1.seleccion.SeleccionID)
                        INNER JOIN db_prueba1.partido ON db_prueba1.PartidoSeleccion.PartidoSeleccionPartidoId = db_prueba1.partido.PartidoID)
                        INNER JOIN db_prueba1.sede ON db_prueba1.partido.PartidoSedesId = db_prueba1.sede.SedeId)
                        WHERE db_prueba1.partido.PartidoID = @PartidoID;
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using(MySqlCommand myCommand=new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoID", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(PartidoSeleccion partidoseleccion)
        {
            string query = @"
                        insert into db_prueba1.PartidoSeleccion (PartidoSeleccionPartidoId, PartidoSeleccionSeleccionId) values
                                                    (@PartidoSeleccionPartidoId, @PartidoSeleccionSeleccionId);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoSeleccionPartidoId", partidoseleccion.PartidoSeleccionPartidoId);
                    myCommand.Parameters.AddWithValue("@PartidoSeleccionSeleccionId", partidoseleccion.PartidoSeleccionSeleccionId);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(PartidoSeleccion partidoseleccion)
        {
            string query = @"
                        update db_prueba1.PartidoSeleccion set 
                        PartidoSeleccionPartidoId =@PartidoSeleccionPartidoId,
                        PartidoSeleccionSeleccionId =@PartidoSeleccionSeleccionId
                        where PartidoSeleccionID=@PartidoSeleccionID;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoSeleccionID", partidoseleccion.PartidoSeleccionID);
                    myCommand.Parameters.AddWithValue("@PartidoSeleccionPartidoId", partidoseleccion.PartidoSeleccionPartidoId);
                    myCommand.Parameters.AddWithValue("@PartidoSeleccionSeleccionId", partidoseleccion.PartidoSeleccionSeleccionId);

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
                        delete from db_prueba1.PartidoSeleccion 
                        where PartidoSeleccionID=@PartidoSeleccionID;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@PartidoSeleccionID", id);

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
