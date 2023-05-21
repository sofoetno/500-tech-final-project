using System.ComponentModel.DataAnnotations;
using FinalProject.Models;

namespace FinalProject;

public class Category
{
    public int Id { get; set; }
    
    /// <summary>
    /// Category name.
    /// </summary>
    [Required]
    public string CategoryName { get; set; }
    
    /// <summary>
    /// Category description.
    /// </summary>
    [Required]
    public string Description { get; set; }
    
    /// <summary>
    /// Products belonging to this category.
    /// </summary>
    public ICollection<Product> Products { get; } = new List<Product>();
}