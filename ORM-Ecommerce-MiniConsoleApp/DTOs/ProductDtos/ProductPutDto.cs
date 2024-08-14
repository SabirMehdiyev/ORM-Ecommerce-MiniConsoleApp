namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class ProductPutDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? Description { get; set; }
}
