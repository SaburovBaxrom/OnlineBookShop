using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopMvc.Data;

namespace OnlineBookShopMvc.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly BookShopDbContext _context;
        public StoreController(BookShopDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string minPrice, string maxPrice)
        {
            var books = _context.Books.Include(b => b.Author).Include(b => b.Category).AsQueryable();
            if(!string.IsNullOrEmpty(searchString) ) 
            {
                books = books.Where(b => b.Name.Contains(searchString) || b.Author.FirstName.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = int.Parse(minPrice);

                books = books.Where(b => b.Price >= min);
            }
            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = int.Parse(maxPrice);

                books = books.Where(b => b.Price <= max);
            }
            return View(await books.ToListAsync());
        }
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
