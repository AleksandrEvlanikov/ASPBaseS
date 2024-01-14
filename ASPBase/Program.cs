
using ASPBase.Models;
using ASPBase.Services;
using MySqlConnector;
using System.Data;

namespace ASPBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureSqlLiteConnection();
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStorageRepository, StoregeRepository>();



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void ConfigureSqlLiteConnection()
        {
            string connectionString = "Server=localhost;Port=3306;Database=ASPBase1Sem;User ID=root;Password=123456789Sasha;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                PrepareSchema(connection);
            }
        }

        private static void PrepareSchema(MySqlConnection mySqlConnection)
        {
            using (MySqlCommand mySqlCommand = mySqlConnection.CreateCommand())
            {
                mySqlCommand.CommandText = "DROP TABLE IF EXISTS product";
                mySqlCommand.ExecuteNonQuery();
                mySqlCommand.CommandText = "DROP TABLE IF EXISTS storage";
                mySqlCommand.ExecuteNonQuery();
                mySqlCommand.CommandText = "DROP TABLE IF EXISTS category";
                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText =
                    @"CREATE TABLE Category (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                Name TEXT,
                Description TEXT
            )";
                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText =
                    @"CREATE TABLE Product (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                Name TEXT,
                Description TEXT,
                Price INT,
                CategoryId INT
            )";
                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText =
                    @"CREATE TABLE Storage (
                Id INT AUTO_INCREMENT PRIMARY KEY,
                Name TEXT,
                Description TEXT,
                ProductId INT
            )";
                mySqlCommand.ExecuteNonQuery();
            }

        }
    }
}
