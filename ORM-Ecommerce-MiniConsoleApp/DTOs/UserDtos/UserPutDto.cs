namespace ORM_Ecommerce_MiniConsoleApp.DTOs;

public class UserPutDto
{
    public int Id { get; set; }

    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Address { get; set; }
}
