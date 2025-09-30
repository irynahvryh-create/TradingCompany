using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.DAL.Concrete
{
    public class ProductLogDal : IProductLogDal
    {
        private readonly string _connStr;

        public ProductLogDal(string connStr)
        {
            _connStr = connStr;
        }

        public ProductLog Create(ProductLog log)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"INSERT INTO ProductLog 
                                    (ProductID, OldPrice, NewPrice, Status, Comment, Date)
                                    OUTPUT inserted.LogID
                                    VALUES (@productId, @oldPrice, @newPrice, @status, @comment, @date)";

                cmd.Parameters.AddWithValue("@productId", log.ProductID);
                cmd.Parameters.AddWithValue("@oldPrice", (object?)log.OldPrice ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@newPrice", (object?)log.NewPrice ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", log.Status);
                cmd.Parameters.AddWithValue("@comment", (object?)log.Comment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@date", log.Date);

                log.LogID = (int)cmd.ExecuteScalar();
                return log;
            }
        }

        public ProductLog? GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM ProductLog WHERE LogID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProductLog
                        {
                            LogID = (int)reader["LogID"],
                            ProductID = (int)reader["ProductID"],
                            OldPrice = (decimal)(reader["OldPrice"] as decimal?),
                            NewPrice = (decimal)(reader["NewPrice"] as decimal?),
                            Status = (bool)reader["Status"],
                            Comment = reader["Comment"] as string,
                            Date = (DateTime)reader["Date"]
                        };
                    }
                }
            }
            return null;
        }

        public List<ProductLog> GetAll()
        {
            var logs = new List<ProductLog>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM ProductLog";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new ProductLog
                        {
                            LogID = (int)reader["LogID"],
                            ProductID = (int)reader["ProductID"],
                            OldPrice = (decimal)(reader["OldPrice"] as decimal?),
                            NewPrice = (decimal)(reader["NewPrice"] as decimal?),
                            Status = (bool)reader["Status"],
                            Comment = reader["Comment"] as string,
                            Date = (DateTime)reader["Date"]
                        });
                    }
                }
            }

            return logs;
        }

        public List<ProductLog> GetByProductId(int productId)
        {
            var logs = new List<ProductLog>();

            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = "SELECT * FROM ProductLog WHERE ProductID = @productId";
                cmd.Parameters.AddWithValue("@productId", productId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new ProductLog
                        {
                            LogID = (int)reader["LogID"],
                            ProductID = (int)reader["ProductID"],
                            OldPrice = (decimal)(reader["OldPrice"] as decimal?),
                            NewPrice = (decimal)(reader["NewPrice"] as decimal?),
                            Status = (bool)reader["Status"],
                            Comment = reader["Comment"] as string,
                            Date = (DateTime)reader["Date"]
                        });
                    }
                }
            }

            return logs;
        }

        public bool Update(ProductLog log)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = @"UPDATE ProductLog
                                    SET ProductID = @productId,
                                        OldPrice = @oldPrice,
                                        NewPrice = @newPrice,
                                        Status = @status,
                                        Comment = @comment,
                                        Date = @date
                                    WHERE LogID = @id";

                cmd.Parameters.AddWithValue("@productId", log.ProductID);
                cmd.Parameters.AddWithValue("@oldPrice", (object?)log.OldPrice ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@newPrice", (object?)log.NewPrice ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@status", log.Status);
                cmd.Parameters.AddWithValue("@comment", (object?)log.Comment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@date", log.Date);
                cmd.Parameters.AddWithValue("@id", log.LogID);

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
                cmd.CommandText = "DELETE FROM ProductLog WHERE LogID = @id";
                cmd.Parameters.AddWithValue("@id", id);

                int affectedRows = cmd.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }
    }
}
