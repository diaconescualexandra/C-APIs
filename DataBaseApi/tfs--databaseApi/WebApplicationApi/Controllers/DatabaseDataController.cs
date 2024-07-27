using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseDataController : ControllerBase
    {
        private string _connectionString;
        public DatabaseDataController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Database");
        }


        [HttpGet("getAll")]
        public List <DatabaseData> GetAll()
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString;


            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from date_persoane;";
            sqlCmd.Connection = myConnection;

            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            List<DatabaseData> dd = new List<DatabaseData>();

            while (reader.Read())
            {
                DatabaseData obj = new DatabaseData
                {
                    Id = Convert.ToInt32(reader.GetValue(0)),
                    Nume = reader.GetValue(1).ToString(),
                    Prenume = reader.GetValue(2).ToString(),
                    Varsta = Convert.ToInt32(reader.GetValue(3))
                };

            dd.Add(obj);
        }

            myConnection.Close();
            return dd;
        }

        [HttpGet("{x}/getTopX")]
        public List < DatabaseData > GetTopXPeople(int x)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select top " + x + "* from date_persoane;" ;
            sqlCmd.Connection = myConnection;

            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            List<DatabaseData> dd = new List<DatabaseData>();
            
            while (reader.Read())
            {
                DatabaseData obj = new DatabaseData
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nume = reader["Nume"].ToString(),
                    Prenume = reader["Prenume"].ToString(),
                    Varsta = Convert.ToInt32(reader["Varsta"])
                };

                dd.Add(obj);

            }

            myConnection.Close();
            return dd;
        }

        [HttpPost]
        public void AddPerson(DatabaseData databasedata)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "insert into date_persoane ( Nume, Prenume, Varsta) Values ( @Nume, @Prenume, @Varsta)";
            sqlCmd.Connection = myConnection;

            //sqlCmd.Parameters.AddWithValue("@Id", databasedata.Id);
            sqlCmd.Parameters.AddWithValue("@Nume", databasedata.Nume);
            sqlCmd.Parameters.AddWithValue("@Prenume", databasedata.Prenume);
            sqlCmd.Parameters.AddWithValue("@Varsta", databasedata.Varsta);

            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }


        [HttpDelete]
        
        public void DeletePersonByID(int id)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from date_persoane where Id=" + id + "";
            sqlCmd.Connection = myConnection;

            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        [HttpPut]
        public void UpdatePersonById(int id, DatabaseData databasedata)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = _connectionString;

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "update date_persoane set Nume = @Nume, Prenume = @Prenume, Varsta = @Varsta where Id =" + id;
            sqlCmd.Connection = myConnection;

            sqlCmd.Parameters.AddWithValue("@Nume", databasedata.Nume);
            sqlCmd.Parameters.AddWithValue("@Prenume", databasedata.Prenume);
            sqlCmd.Parameters.AddWithValue("@Varsta", databasedata.Varsta);

            myConnection.Open();
            int updatedRows = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }


    }
}
