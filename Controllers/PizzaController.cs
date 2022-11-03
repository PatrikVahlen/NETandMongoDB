using ContosoPizza.Models;
using ContosoPizzaAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly PizzaService _pizzaService;
    public PizzaController(PizzaService pizzaService) =>
    _pizzaService = pizzaService;

    [HttpGet]
    public async Task<List<Pizza>> Get() =>
        await _pizzaService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Pizza>> Get(string id)
    {
        var pizza = await _pizzaService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        return pizza;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pizza newPizza)
    {
        Console.WriteLine(newPizza.name);
        await _pizzaService.CreateAsync(newPizza);
        return CreatedAtAction(nameof(Create), new { id = newPizza.Id }, newPizza);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Pizza updatedPizza)
    {
        var pizza = await _pizzaService.GetAsync(id);

        if (pizza is null)
        {
            return NotFound();
        }

        updatedPizza.Id = pizza.Id;

        await _pizzaService.UpdateAsync(id, updatedPizza);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var pizza = await _pizzaService.GetAsync(id);

        if (pizza is null)
            return NotFound();

        await _pizzaService.RemoveAsync(id);

        return NoContent();
    }
}