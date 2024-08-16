namespace ORM_Ecommerce_MiniConsoleApp.Services.Interfaces;

public interface IPaymentService
{
    Task MakePaymentAsync(PaymentPostDto paymentDTO);
    Task<List<PaymentGetDto>> GetPaymentsAsync(int userId);
}
