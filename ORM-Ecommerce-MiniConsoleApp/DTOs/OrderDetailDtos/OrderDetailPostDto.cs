namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class OrderDetailPostDto
{
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerItem { get; set; }
}
