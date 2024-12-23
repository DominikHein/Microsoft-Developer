using Microsoft.AspNetCore.Mvc;

namespace BasicEndpoints.Controllers
{
	public class ProductsController : Controller
	{
		public List<string> products = new List<string> { "Product 1", "Product 2", "Product 3" };
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		[Route("/GetProducts")]
		public IActionResult GetProducts()
		{
			return Ok(products);
		}

		[HttpGet]
		[Route("/GetProduct/{id}")]
		public IActionResult GetProduct(int id)
		{
			return Ok($"Product {products[id]}");
		}

		[HttpPost]
		[Route("/AddProduct")]
		public IActionResult AddProduct(string product)
		{
			products.Append(product);
			return Ok("Product added");
		}

		[HttpDelete]
		[Route("/DeleteProduct/{id}")]
		public IActionResult DeleteProduct(int id)
		{
			products.RemoveAt(id);
			return Ok("Product deleted");
		}
	}
}
