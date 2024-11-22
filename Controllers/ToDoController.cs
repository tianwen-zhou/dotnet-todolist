using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Models;

namespace ToDoListApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> _toDoList = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Title = "Talking", IsCompleted = false },
            new ToDoItem { Id = 2, Title = "Speaking", IsCompleted = true },
            new ToDoItem { Id = 3, Title = "Project-Todo", IsCompleted = false },
            new ToDoItem { Id = 4, Title = "Good-Job", IsCompleted = false },
            new ToDoItem { Id = 5, Title = "Thinking", IsCompleted = true },
            new ToDoItem { Id = 6, Title = "Listening", IsCompleted = false },
            new ToDoItem { Id = 7, Title = "Jumping", IsCompleted = true },
            new ToDoItem { Id = 8, Title = "Hiking", IsCompleted = false },
            new ToDoItem { Id = 9, Title = "Walking", IsCompleted = false },
            new ToDoItem { Id = 10, Title = "Watching", IsCompleted = true }
        };
        
        private static int _nextId = 1;

        // Get all ToDo items
        [HttpGet]
        public ActionResult<IEnumerable<ToDoItem>> GetAll()
        {
            return Ok(_toDoList);
        }

        // Get a single ToDo item by ID
        [HttpGet("{id}")]
        public ActionResult<ToDoItem> GetById(int id)
        {
            var item = _toDoList.FirstOrDefault(t => t.Id == id);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        // Add a new ToDo item
        [HttpPost]
        public ActionResult<ToDoItem> Add(ToDoItem newItem)
        {
            newItem.Id = _nextId++;
            _toDoList.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // Update an existing ToDo item
        [HttpPut("{id}")]
        public IActionResult Update(int id, ToDoItem updatedItem)
        {
            var item = _toDoList.FirstOrDefault(t => t.Id == id);
            if (item == null)
                return NotFound();

            item.Title = updatedItem.Title;
            item.IsCompleted = updatedItem.IsCompleted;
            return NoContent();
        }

        // Delete a ToDo item
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _toDoList.FirstOrDefault(t => t.Id == id);
            if (item == null)
                return NotFound();

            _toDoList.Remove(item);
            return NoContent();
        }
    }
}
