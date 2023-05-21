namespace FinalProject.Dto;

public class TransactionCreateDto
{
    public string TransactionType { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public int UserAccountId { get; set; }
}