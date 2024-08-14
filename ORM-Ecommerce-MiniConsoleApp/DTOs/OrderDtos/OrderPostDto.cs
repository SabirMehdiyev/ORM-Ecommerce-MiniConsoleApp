namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class OrderPostDto
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public int UserId { get; set; }
}
