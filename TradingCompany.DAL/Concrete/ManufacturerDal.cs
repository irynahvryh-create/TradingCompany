using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Concrete
{
    public class ManufacturerDal : IManufacturerDal
    {
        private readonly string _connStr;

        public ManufacturerDal(string connStr)
        {
            _connStr = connStr;
        }

        public Manufacturer Create(Manufacturer manufacturer)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "INSERT INTO Manufacturer (Name, Country) OUTPUT inserted.ManufacturerID VALUES (@name, @country)";
                cmd.Parameters.AddWithValue("@name", manufacturer.Name);
                cmd.Parameters.AddWithValue("@country", manufacturer.Country);

                manufacturer.ManufacturerID = (int)cmd.ExecuteScalar();
                return manufacturer;
            }
        }

        public Manufacturer? GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM Manufacturer WHERE ManufacturerID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Manufacturer
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

        public List<Manufacturer> GetAll()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM Manufacturer";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        manufacturers.Add(new Manufacturer
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

        public bool Update(Manufacturer manufacturer)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "UPDATE Manufacturer SET Name = @name, Country = @country WHERE ManufacturerID = @id";
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
                cmd.CommandText = "DELETE FROM Manufacturer WHERE ManufacturerID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }
    }
}
