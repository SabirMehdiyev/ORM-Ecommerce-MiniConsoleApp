namespace ORM_Ecommerce_MiniConsoleApp.Exceptions;

public class OrderAlreadyCompletedException:Exception
{
    public OrderAlreadyCompletedException(string message):base(message)
    {
        
    }
}
