using AlirezaEShop.Data;
using AlirezaEShop.Models;

//using AlirezaEShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace AlirezaEShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AlirezaEShopContext _context;

        public HomeController(ILogger<HomeController> logger, AlirezaEShopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products
                .ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var products = _context.Products.
                Include(p => p.item)
                .SingleOrDefault(p => p.ID == id);

            if (products == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.ID == id)
                .SelectMany(c => c.CategoryToProduct)
                .Select(ca => ca.category)
                .ToList();
            var vm = new DetailsViewModel()
            {
                Category = categories,
                product = products
            };

            return View(vm);
        }

        [Authorize]
        public IActionResult AddToCart(int ItemID)
        {
            var product = _context.Products
                .Include(p => p.item)
                .SingleOrDefault(p => p.itemID == ItemID);
            if (product != null)
            {

                int UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.Orders.FirstOrDefault(p => p.UserID == UserID && !p.IsFinal);
                if (order != null)
                {
                    var orderDetail = _context.orderDetails.FirstOrDefault(o => o.OrderID == order.OrderID && o.ProductID == product.ID);
                    if (orderDetail != null)
                    {
                        orderDetail.Count += 1;
                    }
                    else
                    {
                        _context.Add(new OrderDetails
                        {
                            OrderID = order.OrderID,
                            ProductID = product.ID,
                            Price = product.item.price,
                            Count = 1
                        });
                    }
                    _context.SaveChanges();
                }
                else
                {
                    order = new Order
                    {
                        IsFinal = false,
                        CreateDate = DateTime.Now,
                        UserID = UserID,
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    _context.Add(new OrderDetails
                    {
                        OrderID = order.OrderID,
                        ProductID = product.ID,
                        Price = product.item.price,
                        Count = 1
                    });
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("ShowCart");
        }

        [Authorize]
        public IActionResult ShowCart()
        {
            int UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Orders.Where(o => o.UserID == UserID && !o.IsFinal)
                .Include(o => o.orderDetails)
                .ThenInclude(o => o.product).FirstOrDefault();
            return View(order);
        }

        public IActionResult RemoveCart(int DetailID)
        {
            var orderDetail = _context.orderDetails.Find(DetailID);
            _context.Remove(orderDetail);
            _context.SaveChanges();
            return RedirectToAction("ShowCart");
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
