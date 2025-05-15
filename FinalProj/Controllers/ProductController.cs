using System.Diagnostics;
using FinalProj.Data;
using FinalProj.Models;
using FinalProj.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Controllers
{
    public class ProductController : Controller
    {

        private ECContext _context;

        public ProductController(ECContext context)
        {
            _context = context;
        }

        public IActionResult ShowProductAll()
        {
            var prod = _context.Products.ToList();
            return View(prod);
        }

        public IActionResult ShowProductCateg(int id)
        {
            var cat = _context.Categories
                              .Include(c => c.Products)
                              .FirstOrDefault(c => c.CategoryId == id);
            return View(cat);
        }

        public IActionResult ShowProductDetails(int id)
        {
            var product = _context.Products
                                  .Include(p => p.Category)
                                  .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            var productReview = _context.Reviews
                      .Include(r => r.Product)
                      .FirstOrDefault(p => p.ProductId == id);

            if (productReview == null)
                return NotFound();

            var relatedProducts = _context.Products
                                          .Where(p => p.CategoryId == product.CategoryId && p.ProductId != id)
                                          .ToList();

            var viewModel = new ProductDetailed
            {
                productDetailed = product,
                relatedproducts = relatedProducts,
                review=productReview,
            };
                
            return View(viewModel);
        }

    }
}
