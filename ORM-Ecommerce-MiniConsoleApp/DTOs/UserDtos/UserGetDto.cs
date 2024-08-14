namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class UserGetDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; }
    public override string ToString()
    {
        if (Address != null)
        {
            return $"{Id}-{FullName}-{Email} - {Address}";
        }
        return $"{Id}-{FullName}-{Email}";
    }
}
