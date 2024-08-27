using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext _context;
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ToDoContext context) => _context = context;

        public IActionResult Index(string id)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Statuses = _context.Statuss.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;

            IQueryable<ToDo> query = _context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status);

            if(filters.HasCategory)
            {
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            }
            
            if(filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            if(filters.HasDue)
            {
                var today = DateTime.Today;
                if(filters.IsPast)
                {
                    query = query.Where(t => t.DueTime < today);
                }
                else if(filters.IsToday)
                {
                    query = query.Where(t => t.DueTime == today);
                }
                else if(filters.IsFuture)
                {
                    query = query.Where(t => t.DueTime > today);
                }
            }
            
            var tasks = query.OrderBy(t => t.DueTime).ToList();

            return View(tasks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Statuses = _context.Statuss.ToList();
            var task = new ToDo { StatusId = "open" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if(ModelState.IsValid)
            {
                _context.ToDos.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Statuses = _context.Statuss.ToList();
                return View(task);
            }
        }


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
