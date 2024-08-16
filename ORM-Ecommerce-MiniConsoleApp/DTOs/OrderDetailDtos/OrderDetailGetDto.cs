namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class OrderDetailGetDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerItem { get; set; }

    public override string ToString()
    {
        return $"OrderId:{OrderId}-Quantity:{Quantity} - PricePerItem:{PricePerItem}";
    }
}
