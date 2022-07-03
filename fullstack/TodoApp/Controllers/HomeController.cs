using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TodoService todoService;

        public HomeController(ILogger<HomeController> logger, TodoService todoService)
        {
            _logger = logger;
            this.todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TodoModel> todolist = await todoService.GetAsync();
            return View(todolist);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Todo item)
        {
            TodoModel todo = new TodoModel();
            todo.Text = item.newTodo;
            await todoService.CreateAsync(todo);
            List<TodoModel> todolist = await todoService.GetAsync();
            return View(todolist);
        }

        public IActionResult Privacy()
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