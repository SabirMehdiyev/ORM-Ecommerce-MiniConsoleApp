namespace ORM_Ecommerce_MiniConsoleApp.Models;

public class Order:BaseEntity
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public int UserId { get; set; }
    public  User User { get; set; }
    public  List<OrderDetail> OrderDetails { get; set; }
    public List<Payment> Payments { get; set; }
}
