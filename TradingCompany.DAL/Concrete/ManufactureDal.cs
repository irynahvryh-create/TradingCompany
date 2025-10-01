using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Concrete
{
    public class ManufactureDal : IManufactureDal
    {
        private readonly string _connStr;

        public ManufactureDal(string connStr)
        {
            _connStr = connStr;
        }



        public Manufacture Create(Manufacture manufacturer)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Manufacture (Name, Country) OUTPUT inserted.ManufacturerID VALUES (@name, @country)";
                cmd.Parameters.AddWithValue("@name", manufacturer.Name);
                cmd.Parameters.AddWithValue("@country", manufacturer.Country);

                manufacturer.ManufacturerID = (int)cmd.ExecuteScalar();
                return manufacturer;
            }
        }

        public Manufacture? GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM Manufacture WHERE ManufacturerID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Manufacture
                        {
                            ManufacturerID = (int)reader["ManufacturerID"],
                            Name = (string)reader["Name"],
                            Country = (string)reader["Country"]
                        };
                    }
                }
                return null;
            }
        }

        public List<Manufacture> GetAll()
        {
            List<Manufacture> manufacturers = new List<Manufacture>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM Manufacture";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        manufacturers.Add(new Manufacture
                        {
                            ManufacturerID = (int)reader["ManufacturerID"],
                            Name = (string)reader["Name"],
                            Country = (string)reader["Country"]
                        });
                    }
                }
            }

            return manufacturers;
        }

        public bool Update(Manufacture manufacturer)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Manufacture SET Name = @name, Country = @country WHERE ManufacturerID = @id";
                cmd.Parameters.AddWithValue("@name", manufacturer.Name);
                cmd.Parameters.AddWithValue("@country", manufacturer.Country);
                cmd.Parameters.AddWithValue("@id", manufacturer.ManufacturerID);

                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }

        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "DELETE FROM Manufacture WHERE ManufacturerID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }

       
    }
}

