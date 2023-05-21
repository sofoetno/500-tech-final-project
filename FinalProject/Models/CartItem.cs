namespace FinalProject.Models;


/// <summary>
/// CartItem is used to contain products in Cart. Cart has CartItems and each CartItem references some product.
/// </summary>
public class CartItem
{
    public int Id { get; set; }
    
    /// <summary>
    /// Product id contained in this cart item.
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Cart id this cart item belongs to.
    /// </summary>
    public int CartId { get; set; }

    /// <summary>
    /// Product contained in this cart item.
    /// </summary>
    public Product Product { get; set; }
    
    /// <summary>
    /// Cart this cart item belongs to.
    /// </summary>
    public Cart Cart { get; set; }

    /// <summary>
    /// Quantity of the product this cart item contains.
    /// </summary>
    public int Count { get; set; }
    
}