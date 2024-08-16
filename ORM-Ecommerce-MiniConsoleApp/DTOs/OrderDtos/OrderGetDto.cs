namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class OrderGetDto
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public int UserId { get; set; }
    public List<OrderDetailGetDto> OrderDetails { get; set; }
    public override string ToString()
    {
        return $"OrderId:{Id} - OrderDate:{OrderDate} - TotalAmount:{TotalAmount} - Status:{Status} - UserId {UserId}";
    }
}


