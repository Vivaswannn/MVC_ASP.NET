using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListProducts()
        {

            List<Product> products = new List<Product>()
            {
                new Product { ProductCode = 1, ProductName = "Laptop", Fees = 50000 },
                new Product { ProductCode = 2, ProductName = "Smartphone", Fees = 20000 },
                new Product { ProductCode = 3, ProductName = "Tablet", Fees = 15000 },
                new Product { ProductCode = 4, ProductName = "Headphones", Fees = 5000 },
            };
            ViewBag.Products = products;
            return View();
        }
    }
}
