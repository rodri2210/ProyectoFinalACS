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
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        select * from 
                        db_prueba1.Usuario
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
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
        public JsonResult Post(Usuario usuario)
        {
            string query = @"
                        insert into db_prueba1.Usuario (UsuarioEmail, UsuarioContrasena, UsuarioPuntos, UsuarioNombreEquipo) values
                                                    (@UsuarioEmail, @UsuarioContrasena, @UsuarioPuntos, @UsuarioNombreEquipo);
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@UsuarioEmail", usuario.UsuarioEmail);
                    myCommand.Parameters.AddWithValue("@UsuarioContrasena", usuario.UsuarioContrasena);
                    myCommand.Parameters.AddWithValue("@UsuarioPuntos", usuario.UsuarioPuntos);
                    myCommand.Parameters.AddWithValue("@UsuarioNombreEquipo", usuario.UsuarioNombreEquipo);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Usuario usuario)
        {
            string query = @"
                        update db_prueba1.Usuario set 
                        UsuarioEmail =@UsuarioEmail,
                        UsuarioContrasena =@UsuarioContrasena,
                        UsuarioPuntos =@UsuarioPuntos,
                        UsuarioNombreEquipo =@UsuarioNombreEquipo
                        where UsuarioID=@UsuarioID;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                    myCommand.Parameters.AddWithValue("@UsuarioEmail", usuario.UsuarioEmail);
                    myCommand.Parameters.AddWithValue("@UsuarioContrasena", usuario.UsuarioContrasena);
                    myCommand.Parameters.AddWithValue("@UsuarioPuntos", usuario.UsuarioPuntos);
                    myCommand.Parameters.AddWithValue("@UsuarioNombreEquipo", usuario.UsuarioNombreEquipo);

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
                        delete from db_prueba1.Usuario 
                        where UsuarioID=@UsuarioID;
                        
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@UsuarioID", id);

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
