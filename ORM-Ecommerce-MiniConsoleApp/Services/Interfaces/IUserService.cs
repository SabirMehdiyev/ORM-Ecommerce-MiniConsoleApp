namespace ORM_Ecommerce_MiniConsoleApp.Services.Interfaces;

public interface IUserService
{
    Task RegisterUserAsync(UserPostDto newUser);
    Task UpdateUserInfoAsync(UserPutDto newUser);
    Task DeleteUserAsync(int id);
    Task<List<UserGetDto>> GetAllUsersAsync();
    Task<UserGetDto> GetUserByIdAsync(int id);
    Task<UserGetDto> LoginAsync(string email, string password);
    Task<List<OrderGetDto>> GetUserOrdersAsync(int userId);
    //Task<string> ExportUserOrdersToExcel(int userId);
}
