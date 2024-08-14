
namespace ORM_Ecommerce_MiniConsoleApp.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    public OrderService()
    {
        _orderRepository = new OrderRepository();
        _userRepository = new UserRepository();
    }



    public async Task CancelOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);
        if (order == null)
            throw new NotFoundException("Order not found.");

        if (order.Status == OrderStatus.Cancelled)
            throw new OrderAlreadyCancelledException("Order is already cancelled.");

        order.Status = OrderStatus.Cancelled;
        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task CompleteOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId);
        if (order == null)
            throw new NotFoundException("Order not found.");

        if (order.Status == OrderStatus.Completed)
            throw new OrderAlreadyCompletedException("Order is already completed.");

        order.Status = OrderStatus.Completed;
        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task CreateOrderAsync(OrderPostDto orderDto)
    {
        if (orderDto.TotalAmount < 0)
            throw new InvalidOrderException("Order amount cannot be less than zero.");

        var user = await _userRepository.GetSingleAsync(u => u.Id == orderDto.UserId);
        if (user == null)
            throw new InvalidOrderException("User not found.");

        if ((int)orderDto.Status < 1 || (int)orderDto.Status >3)
        {
            throw new InvalidOrderException("Ivalid status value!");
        }

        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            TotalAmount = orderDto.TotalAmount,
            Status = OrderStatus.Pending,
            UserId = orderDto.UserId
        };

        await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveChangesAsync();
    }

    public async Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId, "Order.OrderDetails");
        if (order == null)
            throw new NotFoundException("Order not found.");

        var orderDetailDtos = order.OrderDetails.Select(od => new OrderDetailGetDto
        {
            Id = od.Id,
            Quantity = od.Quantity,
            OrderId = od.OrderId,
            ProductId = od.ProductId,
            PricePerItem = od.PricePerItem,
            ProductName = od.Product.Name 
        }).ToList();

        return orderDetailDtos;
    }

    public async Task<List<OrderGetDto>> GetOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetAllAsync();
        var orderDtos = new List<OrderGetDto>();

        foreach (var order in orders)
        {
            var orderDto = new OrderGetDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId
            };

            orderDtos.Add(orderDto);
        }

        return orderDtos;
    }


}
