namespace ORM_Ecommerce_MiniConsoleApp.Services.Interfaces;

public interface IProductService
{
    Task AddProductAsync(ProductPostDto newProduct);
    Task UpdateProductAsync(ProductPutDto newProduct);
    Task DeleteProductAsync(int id);
    Task<ProductGetDto> GetProductByIdAsync(int id);
    Task<List<ProductGetDto>> GetAllProductsAsync();
    Task<List<ProductGetDto>> SearchProductsAsync(string searchQuery);
}
