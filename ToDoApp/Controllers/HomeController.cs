using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext _context;
        //private readonly ILogger<HomeController> _logger;


        public HomeController(ToDoContext context) => _context = context;

        public IActionResult Index(string id, string sortOrder)
        {
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Statuses = _context.Statuss.ToList();
            ViewBag.DueFilters = Filters.DueFilterValues;
            ViewBag.Priorities = _context.Priorities.ToList();

			// set current sort oder 
			ViewBag.CurrentSort = sortOrder;

			IQueryable<ToDo> query = _context.ToDos
                .Include(t => t.Category)
                .Include(t => t.Status);

            if (filters.HasCategory)
            {
                query = query.Where(t => t.CategoryId == filters.CategoryId);
            }

            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }

            if (filters.HasDue)
            {
                var today = DateTime.Today;
                if (filters.IsPast)
                {
                    query = query.Where(t => t.DueTime < today);
                }
                else if (filters.IsToday)
                {
                    query = query.Where(t => t.DueTime == today);
                }
                else if (filters.IsFuture)
                {
                    query = query.Where(t => t.DueTime > today);
                }
            }

            if (filters.HasPriority)
			{
				query = query.Where(t => t.PriorityId == filters.PriorityId);
			}
			// Apply sorting
			switch (sortOrder)
			{
				case "date_desc":
					query = query.OrderByDescending(t => t.DueTime);
					break;
				case "priority_desc":
					query = query.OrderByDescending(t => t.PriorityId == "high" ? 3 : t.PriorityId == "normal" ? 2 : 1);
                    break;
                case "priority_asc":
                    query = query.OrderBy(t => t.PriorityId == "high" ? 3 : t.PriorityId == "normal" ? 2 : 1);
					break;
				default:
					query = query.OrderBy(t => t.DueTime);
					break;
			}

			var tasks = query.ToList();

			return View(tasks);
        }


		[HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Statuses = _context.Statuss.ToList();
            ViewBag.Priorities = _context.Priorities.ToList();
            var task = new ToDo { StatusId = "open", PriorityId = "low" };
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDo task)
        {
            if (ModelState.IsValid)
            {
                _context.ToDos.Add(task);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.Statuses = _context.Statuss.ToList();
                ViewBag.Priorities = _context.Priorities.ToList();
                return View(task);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            var id = string.Join('-', filter);
            return RedirectToAction("Index", new { Id = id });
        }

        [HttpPost]
        // FromRoute attribute: hey get the id from the URl and pass it to the method
        public IActionResult MarkComplete([FromRoute] string id, ToDo selected)
        {
            selected = _context.ToDos.Find(selected.Id);
            if (selected != null)
            {
                selected.StatusId = "closed";
                _context.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = id });
        }

        public IActionResult DeleteComplete([FromRoute] string id)
        {
            var toDelete = _context.ToDos.Where(t => t.StatusId == "closed").ToList();
            foreach (var todo in toDelete)
            {
                _context.ToDos.Remove(todo);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { Id = id });
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
