namespace FinalProject.Models;

/// <summary>
/// Each Payment is related to some Order. Having Payment means that order fulfilled.
/// </summary>
public class Payment
{
    public int Id { get; set; }
    
    /// <summary>
    /// Payment amount.
    /// </summary>
    public int Amount { get; set; }
    
    /// <summary>
    /// Date payment actually processed.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// User id responsible for this payment.
    /// </summary>
    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; }
    
    /// <summary>
    /// Order id this payment was made for.
    /// </summary>
    public int OrderId { get; set; }
    public Order Order { get; set; }

}