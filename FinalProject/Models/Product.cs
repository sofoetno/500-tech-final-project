namespace FinalProject.Models;

public class Product
{
    public int Id { get; set; }

    /// <summary>
    /// Product name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Product price.
    /// </summary>
    public int Price { get; set; }

    /// <summary>
    /// Quantity of available products of this kind.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Product description.
    /// </summary>
    public string Description { get; set; }
    public Category Category { get; set; }
    
    /// <summary>
    /// Product category id.
    /// </summary>
    public int CategoryId { get; set; }
}