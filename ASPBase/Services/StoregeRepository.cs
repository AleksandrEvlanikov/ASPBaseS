using ASPBase.Models;
using MySqlConnector;

namespace ASPBase.Services
{
    public class StoregeRepository : IStorageRepository
    {
        private const string connectionString = "Server=localhost;Port=3306;Database=ASPBase1Sem;User ID=root;Password=123456789Sasha;";


        public int Create(Storage item)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            using MySqlCommand command = new MySqlCommand("INSERT INTO storage(Name, Description, ProductId) VALUES(@Name, @Description,  @ProductId)", connection);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@ProductId", item.ProductId);
            
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


        public int Update(Storage item)
        {
            using MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();

            using MySqlCommand command = new MySqlCommand("UPDATE storage SET Name = @Name, Description = @Description, ProductId = @ProductId WHERE Id=@Id", connection);

            command.Parameters.AddWithValue("@Id", item.Id);
            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@ProductId", item.ProductId);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public IList<Storage> GetAll()
        {
            List<Storage> list = new List<Storage>();
            using MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("SELECT * FROM storage", connection);
            command.Prepare();
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Storage client = new Storage();
                client.Id = reader.GetInt32(0);
                client.Name = reader.GetString(1);
                client.Description = reader.GetString(2);
                //client.Storages = client.GetStoragesForProduct(3);
                client.ProductId = reader.GetInt32(3);
                list.Add(client);
            }
            return list;
        }

        public Storage GetById(int id)
        {
            using MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("SELECT * FROM storage WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Prepare();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Storage client = new Storage();
                client.Id = reader.GetInt32(0);
                client.Name = reader.GetString(1);
                client.Description = reader.GetString(2);
                //client.Storages = client.GetStoragesForProduct(3);
                //client.Price = reader.GetInt32(3);
                client.ProductId = reader.GetInt32(3);
                return client;
            }
            return null;
        }

        public int Delete(int id)
        {
            using MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("DELETE FROM storage WHERE Id=@Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }

        public int DeleteAll()
        {
            using MySqlConnection connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
            connection.Open();
            using MySqlCommand command =
                new MySqlCommand("DELETE FROM storage", connection);
            //command.Parameters.AddWithValue("@ClientId", id);
            command.Prepare();
            return command.ExecuteNonQuery();
        }
    }
}
