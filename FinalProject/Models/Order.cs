namespace FinalProject.Models;

/// <summary>
/// Each Order is related to some Cart. Having Order means that corresponding Cart items was sold out.
/// </summary>
public class Order
{
    public int Id { get; set; }
    
    /// <summary>
    /// Date order generated.
    /// </summary>
    public DateTime Date { get; set; }
    
    /// <summary>
    /// Cart id this order is generated for.
    /// </summary>
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    public Delivery? Delivery { get; set; }
}