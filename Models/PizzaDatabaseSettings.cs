namespace PizzaStoreApi.Models;

public class PizzaStoreDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PizzaCollectionName { get; set; } = null!;
}