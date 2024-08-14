namespace ORM_Ecommerce_MiniConsoleApp.Models;

public class Payment:BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int OrderId { get; set; }
    public  Order Order { get; set; }
}
