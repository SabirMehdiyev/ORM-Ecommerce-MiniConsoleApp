namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class PaymentGetDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public Order? Order { get; set; }
    public override string ToString()
    {
        return $"PaymentId:{Id} - OrderId:{OrderId} - Amount:{Amount} - PaymentDate{PaymentDate}";
    }
}


