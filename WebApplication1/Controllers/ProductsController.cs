
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var products = new List<Product>();

            var connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return StatusCode(500,
                    "Chua cau hinh ket noi database.");
            }

            using var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();

            const string sql = @"
                SELECT TOP (20)
                    ProductID,
                    ProductName,
                    Price,
                    StockQuantity
                FROM dbo.Products
                ORDER BY ProductID";

            using var command = new SqlCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    ProductID = reader.GetInt32(0),
                    ProductName = reader.IsDBNull(1)
                        ? null : reader.GetString(1),
                    Price = reader.IsDBNull(2)
                        ? null : reader.GetDecimal(2),
                    StockQuantity = reader.IsDBNull(3)
                        ? null : reader.GetInt32(3)
                });
            }

            return View(products);
        }
    }
}
