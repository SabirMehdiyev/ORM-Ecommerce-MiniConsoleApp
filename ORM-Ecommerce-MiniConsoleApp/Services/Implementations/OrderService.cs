namespace ORM_Ecommerce_MiniConsoleApp.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;
    private readonly IProductRepository _productRepository;
    public OrderService()
    {
        _orderRepository = new OrderRepository();
        _userRepository = new UserRepository();
        _productRepository = new ProductRepository();
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
        if (orderDto.OrderDetails == null || !orderDto.OrderDetails.Any())
            throw new InvalidOrderException("Order must have at least one order detail.");

        var user = await _userRepository.GetSingleAsync(u => u.Id == orderDto.UserId);
        if (user == null)
            throw new InvalidOrderException("User not found.");

        var order = new Order
        {
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0,  
            Status = OrderStatus.Pending,
            UserId = orderDto.UserId,
            OrderDetails = new List<OrderDetail>()  
        };

        await _orderRepository.CreateAsync(order);
        await _orderRepository.SaveChangesAsync();

        foreach (var detail in orderDto.OrderDetails)
        {
            await AddOrderDetailAsync(order.Id, detail);
        }
    }


    public async Task<List<OrderDetailGetDto>> GetOrderDetailsByOrderIdAsync(int orderId)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId, "OrderDetails");
        if (order == null)
            throw new NotFoundException("Order not found.");

        var orderDetailDtos = order.OrderDetails.Select(od => new OrderDetailGetDto
        {
            Id = od.Id,
            Quantity = od.Quantity,
            OrderId = od.OrderId,
            ProductId = od.ProductId,
            PricePerItem = od.PricePerItem
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
    public async Task AddOrderDetailAsync(int orderId, OrderDetailPostDto orderDetailDto)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == orderId, "OrderDetails");
        if (order == null)
            throw new NotFoundException("Order not found.");

        var product = await _productRepository.GetSingleAsync(p => p.Id == orderDetailDto.ProductId);
        if (product == null)
            throw new NotFoundException("Product not found.");

        if (product.Stock < orderDetailDto.Quantity)
            throw new InvalidOrderDetailException("Not enough stock for product.");

        product.Stock -= orderDetailDto.Quantity;
        _productRepository.Update(product);

        var orderDetail = new OrderDetail
        {
            ProductId = orderDetailDto.ProductId,
            Quantity = orderDetailDto.Quantity,
            PricePerItem = orderDetailDto.PricePerItem,
            OrderId = orderId
        };

        order.OrderDetails.Add(orderDetail);
        order.TotalAmount += orderDetailDto.PricePerItem * orderDetailDto.Quantity;
        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync();
    }


}
