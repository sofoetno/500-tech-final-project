using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models;

public class Cart
{
    public int Id { get; set; }
    
    /// <summary>
    /// User id this cart belongs to.
    /// </summary>
    [Required]
    public int UserAccountId { get; set; }
    
    /// <summary>
    /// User this cart belongs to.
    /// </summary>
    [Required]
    public UserAccount UserAccount { get; set; }
    
    /// <summary>
    /// Product Items contained in this cart.
    /// </summary>
    [Required]
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();

    /// <summary>
    /// Order generated for all of the items in this cart. Cart having corresponding order considered as sold out. 
    /// </summary>
    public Order? Order { get; set; }
}