using ORM_Ecommerce_MiniConsoleApp.Models;

namespace ORM_Ecommerce_MiniConsoleApp.Services.Implementations;

public class ProductService:IProductService
{
    private readonly IProductRepository _repository;
    public ProductService()
    {
        _repository = new ProductRepository();
    }

    public async Task AddProductAsync(ProductPostDto newProduct)
    {
        if (newProduct.Name == null)
        {
            throw new InvalidProductException("Product name can't be empty!");
        }
        if (newProduct.Price <= 0)
        {
            throw new InvalidProductException("Product price must be greater than zero!");
        }

        if (newProduct.Stock < 0)
        {
            throw new InvalidProductException("Product stock can't be negative!");
        }

        Product product = new()
        {
            Name = newProduct.Name,
            Price = newProduct.Price,
            Stock = newProduct.Stock,
            Description = newProduct.Description
        };

        await _repository.CreateAsync(product);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _getProductById(id);

         _repository.Delete(product);
        await _repository.SaveChangesAsync();
    }

    public async Task<List<ProductGetDto>> GetAllProductsAsync()
    {
        var products = await _repository.GetAllAsync();

        List<ProductGetDto> ProductsList = new List<ProductGetDto>();
        products.ForEach(product =>
        {
            ProductGetDto productGetDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
            ProductsList.Add(productGetDto);
        });

        return ProductsList;
    }

    public async Task<ProductGetDto> GetProductByIdAsync(int id)
    {
        var product = await _repository.GetSingleAsync(p => p.Id == id);

        if (product == null)
        {
            throw new InvalidProductException("Product not found!");
        }

        return new ProductGetDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Stock = product.Stock,
            Description = product.Description
        };
    }

    public async Task<List<ProductGetDto>> SearchProductsAsync(string searchQuery)
    {
        var result = await _repository.GetAllAsync();
        var filteredProducts = result.Where(p => p.Name.ToLower().Contains(searchQuery.ToLower().Trim())).ToList();
        var productDtos = new List<ProductGetDto>();
        foreach (var product in filteredProducts)
        {
            var productDto = new ProductGetDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description
            };
            productDtos.Add(productDto);
        }
        return productDtos;
    }

    public async Task UpdateProductAsync(ProductPutDto newProduct)
    {
        var product = await _getProductById(newProduct.Id);

        if (newProduct.Name != null)
        {
            product.Name = newProduct.Name;
        }
        if (newProduct.Description != null)
        {
            product.Description = newProduct.Description;
        }
        if (newProduct.Price != null)
        {
            if (newProduct.Price.Value <= 0)
            {
                throw new InvalidProductException("Product price must be greater than zero!");
            }
            product.Price = newProduct.Price.Value;
        }
        if (newProduct.Stock >= 0)
        {
            product.Stock = newProduct.Stock.Value;
        }
        else
        {
            throw new InvalidProductException("Product stock can't be negative!");
        }

        product.UpdatedDate = DateTime.UtcNow;
        _repository.Update(product);
        await _repository.SaveChangesAsync();
    }
    private async Task<Product> _getProductById(int id)
    {
        var product = await _repository.GetSingleAsync(p => p.Id == id);

        if (product == null)
        {
            throw new InvalidProductException("Product not found!");
        }

        return product;
    }
}
