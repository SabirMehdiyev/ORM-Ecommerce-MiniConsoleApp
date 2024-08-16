namespace ORM_Ecommerce_MiniConsoleApp.Services.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(OrderPostDto orderDto);
    Task CancelOrderAsync(int orderId);
    Task CompleteOrderAsync(int orderId);
    Task<List<OrderGetDto>> GetOrdersAsync(int userId);
    Task AddOrderDetailAsync(int orderId, OrderDetailPostDto orderDetailDto);
    Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderIdAsync(int orderId);
}

