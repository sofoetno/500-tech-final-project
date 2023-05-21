namespace FinalProject.Dto;

public class PaymentCreateDto
{
    public int Amount { get; set; }
    public int UserAccountId { get; set; }
    public int OrderId { get; set; }
}