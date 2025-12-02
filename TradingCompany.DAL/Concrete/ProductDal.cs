using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Concrete
{
    public class ProductDal : IProductDal
    {
        private readonly string _connStr;

        public ProductDal(string connStr)
        {
            _connStr = connStr;
        }

        public Product Create(Product product)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandText = @"INSERT INTO Product 
                                (Name, CategoryID, PriceIn, PriceOut, ManufacturerID, Status) 
                                OUTPUT inserted.ProductID 
                                VALUES (@name, @categoryId, @priceIn, @priceOut, @manufacturerId, @status)";
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@categoryId", (object?)product.CategoryID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@priceIn", product.PriceIn);
            cmd.Parameters.AddWithValue("@priceOut", product.PriceOut);
            cmd.Parameters.AddWithValue("@manufacturerId", product.ManufacturerID);
            cmd.Parameters.AddWithValue("@status", product.Status);

            product.ProductID = (int)cmd.ExecuteScalar()!;
            return product;
        }

        public Product? GetById(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandText = "SELECT * FROM Product WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    ProductID = (int)reader["ProductID"],
                    Name = (string)reader["Name"],
                    CategoryID = reader["CategoryID"] == DBNull.Value ? null : (int?)reader["CategoryID"],
                    PriceIn = (decimal)reader["PriceIn"],
                    PriceOut = (decimal)reader["PriceOut"],
                    ManufacturerID = (int)reader["ManufacturerID"],
                    Status = (bool)reader["Status"]
                };
            }

            return null;
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();

            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandText = "SELECT * FROM Product";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    ProductID = (int)reader["ProductID"],
                    Name = (string)reader["Name"],
                    CategoryID = reader["CategoryID"] == DBNull.Value ? null : (int?)reader["CategoryID"],
                    PriceIn = (decimal)reader["PriceIn"],
                    PriceOut = (decimal)reader["PriceOut"],
                    ManufacturerID = (int)reader["ManufacturerID"],
                    Status = (bool)reader["Status"]
                });
            }

            return products;
        }

        public bool Update(Product product)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandText = @"UPDATE Product 
                                SET Name = @name, CategoryID = @categoryId, PriceIn = @priceIn, 
                                    PriceOut = @priceOut, ManufacturerID = @manufacturerId, Status = @status 
                                WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@name", product.Name);
            cmd.Parameters.AddWithValue("@categoryId", (object?)product.CategoryID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@priceIn", product.PriceIn);
            cmd.Parameters.AddWithValue("@priceOut", product.PriceOut);
            cmd.Parameters.AddWithValue("@manufacturerId", product.ManufacturerID);
            cmd.Parameters.AddWithValue("@status", product.Status);
            cmd.Parameters.AddWithValue("@id", product.ProductID);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            using var conn = new SqlConnection(_connStr);
            using var cmd = conn.CreateCommand();

            conn.Open();
            cmd.CommandText = "DELETE FROM Product WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
