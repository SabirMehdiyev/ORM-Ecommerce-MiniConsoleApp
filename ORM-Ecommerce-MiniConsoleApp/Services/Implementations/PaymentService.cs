namespace ORM_Ecommerce_MiniConsoleApp.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IOrderRepository _orderRepository;
    public PaymentService()
    {
        _paymentRepository = new PaymentRepository();
        _orderRepository = new OrderRepository();
    }
    public async Task<List<PaymentGetDto>> GetPaymentsAsync(int userId)
    {
        //var payments = await _paymentRepository.GetAllAsync("Order");  yoxla

        var userPayments =await _paymentRepository.GetFilterAsync(x=>x.Order.UserId == userId,"Order");
        var paymentDtos = userPayments.Select(p => new PaymentGetDto
        {
            Id = p.Id,
            OrderId = p.OrderId,
            Amount = p.Amount,
            PaymentDate = p.PaymentDate
        }).ToList();

        return paymentDtos;
    }



    public async Task MakePaymentAsync(PaymentPostDto paymentDTO)
    {
        var order = await _orderRepository.GetSingleAsync(o => o.Id == paymentDTO.OrderId);

        if (order == null)
        {
            throw new NotFoundException("Order not found.");
        }

        if (paymentDTO.Amount <= 0)
        {
            throw new InvalidPaymentException("Payment amount must be greater than zero.");
        }

        var payment = new Payment
        {
            OrderId = paymentDTO.OrderId,
            Amount = paymentDTO.Amount,
            PaymentDate = DateTime.UtcNow
            
        };

        await _paymentRepository.CreateAsync(payment);
        await _paymentRepository.SaveChangesAsync();
    }

}
