namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class ProductGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        if (Description is null)
        {
            return $"{Id}-{Name}-{Price}-{Stock}";
        }
        else
        {
            return $"{Id}-{Name}-{Price}-{Stock}-{Description}";
        }
    }
}
