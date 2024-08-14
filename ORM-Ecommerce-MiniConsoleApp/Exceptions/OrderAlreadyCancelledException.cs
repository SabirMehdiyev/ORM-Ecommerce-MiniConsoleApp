namespace ORM_Ecommerce_MiniConsoleApp.Exceptions;

public class OrderAlreadyCancelledException:Exception
{
    public OrderAlreadyCancelledException(string message):base(message)
    {
        
    }
}
