
using ASPBase.GraphQL;
using ASPBase.Services;
using Microsoft.Extensions.FileProviders;
using MySqlConnector;
using HotChocolate.AspNetCore;




namespace ASPBase
{

    public class Program
    {
        
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IStorageRepository, StoregeRepository>();

            builder.Services.AddScoped<MySqlConnection>(provider =>
            {
                var connectionString = builder.Configuration.GetConnectionString("db");
                return new MySqlConnection(connectionString);
            });





            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();


            builder.Services.AddGraphQLServer()
                .AddQueryType<RequestGraphQL>();

            var app = builder.Build();


            var staticFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "StaticFile");
            Directory.CreateDirectory(staticFilePath);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(staticFilePath),
                RequestPath = "/static"
            });


            ConfigureMySqlConnection(app);




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();
            app.MapGraphQL();

            app.Run();
        }

        private static void ConfigureMySqlConnection(WebApplication app)
        {
            string connectionString = app.Configuration.GetConnectionString("db");
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
