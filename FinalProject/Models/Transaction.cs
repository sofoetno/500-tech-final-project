namespace FinalProject.Models;

public class Transaction
{
    public int Id { get; set; }
    public string TransactionType { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; }
}