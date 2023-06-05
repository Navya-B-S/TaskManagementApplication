using Microsoft.AspNetCore.Mvc;
using TaskManagementApplication.Models;
using Task = TaskManagementApplication.Models.Task;

namespace TaskManagementApplication.Controllers
{
    public class TaskController : Controller
    {
        //Reference the DataBaseContext class
        private readonly DataBaseContext _context;

        //Add a constructor that accepts DataBaseContext as a parameter and assign it to the _context field
        public TaskController(DataBaseContext context)
        {
            _context = context;
        }

        //Add an Index action method with appropriate Route Attribute that returns a view that lists all the tasks
        [Route(" ")]
        [Route("~/")]
        [Route("Task/Index")]
        public IActionResult Index()
        {
            //get all the tasks from the Tasks table
            var tasks = _context.Tasks.ToList();

            //return the view by passing the tasks list to the view
            return View(tasks);
        }

        //Add a Details Action method with appropriate Route Attribute that returns a view that displays the details of a task
        [Route("Task/Details/{id}")]
        public IActionResult Details(int id)
        {
            //get the task from the Tasks table whose TaskId matches the id parameter
            var task = _context.Tasks.Find(id);

            //return the view by passing the task object to the view
            return View(task);
        }

        //Add a create action method and return Task model and Add text to the view
        [Route("Task/Create")]  
        public IActionResult Create()
        {
            return View("Create", new Task());
        }

        //Add a Create Action method with appropriate Route Attribute and Saves the task to the Tasks table and redirects to the Index action method
        [Route("Task/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Task task)
        {
            //check if the model is valid
            if (ModelState.IsValid)
            {
                //add the task to the Tasks table
                _context.Tasks.Add(task);

                //save the changes to the database
                _context.SaveChanges();

                //redirect to the Index action method
                return RedirectToAction("Index");
            }

            //return the view
            return View(task);
        }

        //Add an Edit action method with appropriate Route attribute and return the view that displays the details of a task
        [Route("Task/Edit/{id}")]
        public IActionResult Edit(int id)
        {
            //get the task from the Tasks table whose TaskId matches the id parameter
            var task = _context.Tasks.Find(id);

            //return the view by passing the task object to the view
            return View(task);
        }

        //Add an Edit Action with appropiate Route attribute and updates the task in the Tasks table and redirects to the Index action method
        [Route("Task/Edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Task task)
        {
            //check if the model is valid
            if (ModelState.IsValid)
            {
                //set the value of the TaskId field of the task object to the id parameter
                task.TaskId = id;

                //set the state of the task object to modified
                _context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                //update the task in the Tasks table
                _context.Tasks.Update(task);

                //save the changes to the database
                _context.SaveChanges();

                //redirect to the Index action method
                return RedirectToAction("Index");
            }

            //return the view
            return View(task);
        }

        //Add delete action to remove the record from the database and redirects to the Index action method
        [Route("Task/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            //get the task from the Tasks table whose TaskId matches the id parameter
            var task = _context.Tasks.Find(id);

            //remove the task from the Tasks table
            _context.Tasks.Remove(task);

            //save the changes to the database
            _context.SaveChanges();

            //redirect to the Index action method
            return RedirectToAction("Index");
        }
    }
}
