using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookShopMvc.Data;

namespace OnlineBookShopMvc.Controllers
{
    public class StoreController : Controller
    {
        private readonly BookShopDbContext _context;
        public StoreController(BookShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookShopDbContext = _context.Books.Include(b => b.Author).Include(b => b.Category);
            return View(await bookShopDbContext.ToListAsync());
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
