using Microsoft.AspNetCore.Mvc;
using ShopApp.Models;

namespace ShopApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private static List<Item> _items = new()
    {
        new Item { Id = 1, Name = "Pen" },
        new Item { Id = 2, Name = "Pencil" },
        new Item { Id = 3, Name = "Notebook" }
    };
    private static int _nextId = 4;

    [HttpGet]
    public IActionResult GetAll() => Ok(_items);

    [HttpGet("{id:int}")]
    public IActionResult GetById(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        return item == null ? NotFound("Item not found") : Ok(item);
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required");

        return Ok(_items.Where(i => i.Name.Contains(name, StringComparison.OrdinalIgnoreCase)));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Item request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name cannot be empty");

        request.Id = _nextId++;
        _items.Add(request);
        return CreatedAtAction(nameof(GetById), new { id = request.Id }, request);
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Item request)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound("Item not found");
        if (string.IsNullOrWhiteSpace(request.Name)) return BadRequest("Name cannot be empty");

        item.Name = request.Name;
        return Ok(item);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null) return NotFound("Item not found");

        _items.Remove(item);
        return NoContent();
    }
}


