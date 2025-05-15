using FinalProj.Data;
using FinalProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Controllers
{
    public class CartController : Controller
    {
        private readonly ECContext _context;

        public CartController(ECContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login_Signup");
            }

            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.UserId == userId);

            if (cart == null)
            {
                return View(new Cart());
            }

            cart.Total = cart.CartItems.Sum(ci => ci.Price * ci.Quantity);
            cart.ItemTotal = cart.CartItems.Count;

            return View(cart);
        }
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Login_Signup");
            }

            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId.Value, Total = 0, ItemTotal = 0 };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (product.Stock < quantity)
            {
                TempData["Error"] = "Sorry, not enough stock available!";
                return RedirectToAction("ShowProductDetails", "Product", new { id = productId });
            }

            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                // cartitemid not identity
                int newCartItemId;
                do
                {
                    newCartItemId = new Random().Next(1, 1000);
                }
                while (_context.CartItems.Any(ci => ci.CartItemId == newCartItemId));
                cartItem = new CartItem
                {
                    CartItemId = newCartItemId,
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity,
                    Price = product.Price
                };
                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("ShowProductDetails", "Product", new { id = productId });
        }

        public IActionResult RemoveFromCart(int cartItemId)
        {

            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult Checkout(int id)
        {
            var cart = _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.CartId == id);

            if (cart == null)
            {
                return RedirectToAction("Index");
            }

            return View("Checkout", cart);
        }

    }
}
