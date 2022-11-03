namespace ContosoPizzaAPI.Services;
using PizzaStoreApi.Models;
using ContosoPizza.Models;

using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class PizzaService
{
    private readonly IMongoCollection<Pizza> _pizzaCollection;

    public PizzaService(
        IOptions<PizzaStoreDatabaseSettings> pizzaStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            pizzaStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            pizzaStoreDatabaseSettings.Value.DatabaseName);

        _pizzaCollection = mongoDatabase.GetCollection<Pizza>(
            pizzaStoreDatabaseSettings.Value.PizzaCollectionName);
    }

    public async Task<List<Pizza>> GetAsync() =>
        await _pizzaCollection.Find(_ => true).ToListAsync();

    public async Task<Pizza?> GetAsync(string id) =>
        await _pizzaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Pizza newBook) =>
        await _pizzaCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(string id, Pizza updatedPizza) =>
        await _pizzaCollection.ReplaceOneAsync(x => x.Id == id, updatedPizza);

    public async Task RemoveAsync(string id) =>
        await _pizzaCollection.DeleteOneAsync(x => x.Id == id);
}

// public static class PizzaService
// {
//     static List<Pizza> Pizzas { get; }
//     static int nextId = 3;
//     static PizzaService()
//     {
//         Pizzas = new List<Pizza>
//         {
//             new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
//             new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
//         };
//     }

//     public static List<Pizza> GetAll() => Pizzas;

//     public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

//     public static void Add(Pizza pizza)
//     {
//         pizza.Id = nextId++;
//         Pizzas.Add(pizza);
//     }

//     public static void Delete(int id)
//     {
//         var pizza = Get(id);
//         if (pizza is null)
//             return;

//         Pizzas.Remove(pizza);
//     }

//     public static void Update(Pizza pizza)
//     {
//         var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
//         if (index == -1)
//             return;

//         Pizzas[index] = pizza;
//     }
// }