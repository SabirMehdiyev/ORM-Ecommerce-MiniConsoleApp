namespace ORM_Ecommerce_MiniConsoleApp.Models;

public class OrderDetail:BaseEntity
{
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerItem { get; set; }
    public  Order Order { get; set; }
    public  Product Product { get; set; }
}
