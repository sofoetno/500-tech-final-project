namespace FinalProject.Dto;

public class ProductUpdateDto
{
    public string? Name { get; set; }
    public int? Price { get; set; }
    public int? Count { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
}