using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;
using ToDoListApp.Data;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoContext _context;

        public ToDoController(ToDoContext context)
        {
            _context = context;
        }

        // Get all ToDo items
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetAll()
        {
            return Ok(_context.ToDoItems.ToList());
        }

        // Get a single ToDo item by ID
        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetById(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        // Add a new ToDo item
        [HttpPost]
        public ActionResult<ToDoItem> Add(ToDoItem newItem)
        {
            _context.ToDoItems.Add(newItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // Update an existing ToDo item
        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItem updatedItem)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
                return NotFound();

            item.Title = updatedItem.Title;
            item.IsCompleted = updatedItem.IsCompleted;
            _context.SaveChanges();
            return NoContent();
        }

        // Delete a ToDo item
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _context.ToDoItems.Find(id);
            if (item == null)
                return NotFound();

            _context.ToDoItems.Remove(item);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
