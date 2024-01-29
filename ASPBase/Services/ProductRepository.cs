using ASPBase.Models;
using CsvHelper;
using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Text;
using static HotChocolate.ErrorCodes;

namespace ASPBase.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlConnection connection;

        public ProductRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public int Create(Product item)
        {
            //using MySqlConnection connection = new MySqlConnection();
            connection.Open();

            using MySqlCommand command = new MySqlCommand("INSERT INTO product(Name, Description, Price, CategoryId) VALUES(@Name, @Description, @Price, @CategoryId)", connection);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@Price", item.Price);
            command.Parameters.AddWithValue("@CategoryId", item.CategoryId);
            command.Prepare();


            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка!!!: {ex.Message}");
                return -1;
            }
        }


        public int Update(Product item)
        {
            //using MySqlConnection connection = new MySqlConnection();
            //connection.ConnectionString = connectionString;
            connection.Open();

            using MySqlCommand command = new MySqlCommand("UPDATE product SET Name = @Name, Description = @Description, Price = @Price, CategoryId = @CategoryId WHERE Id=@Id", connection);

            command.Parameters.AddWithValue("@Id", item.Id);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@Price", item.Price);
            command.Parameters.AddWithValue("@CategoryId", item.CategoryId);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public IList<Product> GetAll()
        {
            string connectionString = "Server=localhost;Port=3306;Database=ASPBase1Sem;User ID=root;Password=123456789Sasha;";
            List<Product> list = new List<Product>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            //connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("SELECT * FROM product", connection);
            command.Prepare();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Product client = new Product();
                client.Id = reader.GetInt32(0);
                client.Name = reader.GetString(1);
                client.Description = reader.GetString(2);
                //client.Storages = client.GetStoragesForProduct(3);
                client.Price = reader.GetInt32(3);
                client.CategoryId = reader.GetInt32(4);
                list.Add(client);
            }
            return list;
        }

        public Product GetById(int id)
        {
            //using MySqlConnection connection = new MySqlConnection();
            //connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("SELECT * FROM product WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Product client = new Product();
                client.Id = reader.GetInt32(0);
                client.Name = reader.GetString(1);
                client.Description = reader.GetString(2);
                //client.Storages = client.GetStoragesForProduct(3);
                client.Price = reader.GetInt32(3);
                client.CategoryId = reader.GetInt32(4);
                return client;
            }
            return null;
        }

        public int Delete(int id)
        {
            //using MySqlConnection connection = new MySqlConnection();
            //connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("DELETE FROM product WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int DeleteAll()
        {
            //using MySqlConnection connection = new MySqlConnection();
            //connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("DELETE FROM product", connection);
            //command.Parameters.AddWithValue("@ClientId", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public string GetCsv(IEnumerable<Product> products)
        {
            StringBuilder sd = new StringBuilder();
            sd.AppendLine("CategoryId,Name,Description,Price");

            foreach (Product product in products)
            {
                sd.AppendLine(product.CategoryId +
                    "," + product.Name +
                    "," + product.Description +
                    "," + product.Price
                    );
            }
            return sd.ToString();
        }

    }
}
