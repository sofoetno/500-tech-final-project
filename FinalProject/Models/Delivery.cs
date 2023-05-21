namespace FinalProject.Models;

/// <summary>
/// Each Delivery is related to some Order.
/// </summary>
public class Delivery
{
    public int Id { get; set; }

    /// <summary>
    /// Date of delivery.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Order id this delivery is corresponds to.
    /// </summary>
    public int OrderId { get; set; }
    public Order Order;
}