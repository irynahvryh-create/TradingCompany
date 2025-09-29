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
    public class CategoryDal : ICategoryDal
    {
        private readonly string _connStr;

        public CategoryDal(string connStr)
        {
            _connStr = connStr;
        }

        public Category Create(Category category)
        {
            using SqlConnection conn = new (_connStr);
            conn.Open();

            string sql = "INSERT INTO Category (Name, Status) OUTPUT inserted.CategoryID VALUES (@name, @status)";

            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", category.Name);
            cmd.Parameters.AddWithValue("@status", category.Status);

            category.CategoryID = (int)cmd.ExecuteScalar();
            return category;
        }

        public Category? GetById(int id)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();

            string sql = "SELECT * FROM Category WHERE CategoryID = @id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Category
                {
                    CategoryID = (int)reader["CategoryID"],
                    Name = (string)reader["Name"],
                    Status = (bool)reader["Status"]
                };
            }

            return null;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();

            string sql = "SELECT * FROM Category";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                categories.Add(new Category
                {
                    CategoryID = (int)reader["CategoryID"],
                    Name = (string)reader["Name"],
                    Status = (bool)reader["Status"]
                });
            }

            return categories;
        }

        public bool Update(Category category)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();

            string sql = "UPDATE Category SET Name = @name, Status = @status WHERE CategoryID = @id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", category.Name);
            cmd.Parameters.AddWithValue("@status", category.Status);
            cmd.Parameters.AddWithValue("@id", category.CategoryID);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();

            string sql = "DELETE FROM Category WHERE CategoryID = @id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }

}
