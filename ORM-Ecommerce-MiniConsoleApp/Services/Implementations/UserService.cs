﻿using ClosedXML.Excel;

namespace ORM_Ecommerce_MiniConsoleApp.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService()
    {
        _userRepository = new UserRepository();
    }
    public async Task RegisterUserAsync(UserPostDto newUser)
    {
        var IsDuplicateEmail = await _userRepository.IsExistAsync(u => u.Email == newUser.Email.Trim());
        if (IsDuplicateEmail)
        {
            throw new InvalidUserInformationException("User with this email already exists");
        }
        if (newUser.Password.Length < 8)
        {
            throw new InvalidUserInformationException("Password must be at least 8 characters");
        }
        if (newUser.FullName.Trim().Length == 0)
        {
            throw new InvalidUserInformationException("Fullname can't be empty");
        }
        User user = new User
        {
            FullName = newUser.FullName,
            Email = newUser.Email,
            Password = newUser.Password,
            Address = newUser.Address
        };

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangesAsync();
    }
    public async Task<UserGetDto> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetSingleAsync(u => u.Email.ToLower() == email.ToLower());

        if (user == null)
        {
            throw new UserAuthenticationException("Email or password incorrect");
        }

        if (user.Password != password)
        {
            throw new UserAuthenticationException("Email or password incorrect!Please try again!");
        }

        UserGetDto userDto = new UserGetDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Address = user.Address
        };

        return userDto;
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _getUserById(id);
        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<List<UserGetDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        List<UserGetDto> result = new List<UserGetDto>();
        users.ForEach(user =>
        {
            UserGetDto userGetDto = new()
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Address = user.Address
            };
            result.Add(userGetDto);
        });

        return result;
    }

    public async Task<UserGetDto> GetUserByIdAsync(int id)
    {
        var user = await _getUserById(id);

        UserGetDto userDto = new()
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Address = user.Address
        };
        return userDto;
    }

    public async Task UpdateUserInfoAsync(UserPutDto newUser)
    {
        User user = await _getUserById(newUser.Id);

        if (newUser.FullName != null)
        {
            user.FullName = newUser.FullName;
        }
        if (newUser.Email != null)
        {
            user.Email = newUser.Email;
        }
        if (newUser.Address != null)
        {
            user.Address = newUser.Address;
        }
        if (newUser.Password != null)
        {
            user.Password = newUser.Password;
        }

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }
    public async Task<List<OrderGetDto>> GetUserOrdersAsync(int userId)
    {
        var user = await _userRepository.GetSingleAsync(u => u.Id == userId, "Orders");

        if (user == null)
        {
            throw new InvalidUserInformationException("User not found!");
        }

        var userOrders = new List<OrderGetDto>();

        foreach (var order in user.Orders)
        {
            var orderDto = new OrderGetDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                UserId = order.UserId
            };

            userOrders.Add(orderDto);
        }

        return userOrders;
    }

    private async Task<User> _getUserById(int id)
    {
        var user = await _userRepository.GetSingleAsync(x => x.Id == id);

        if (user is null)
            throw new NotFoundException("User is not found");

        return user;
    }

    public async Task<string> ExportUserOrdersToExcel(int userId)
    {
        var userOrders = await GetUserOrdersAsync(userId);

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Orders");

            worksheet.Cell(1, 1).Value = "Order ID";
            worksheet.Cell(1, 2).Value = "Order Date";
            worksheet.Cell(1, 3).Value = "Total Amount";
            worksheet.Cell(1, 4).Value = "Status";

            int row = 2;
            foreach (var order in userOrders)
            {
                worksheet.Cell(row, 1).Value = order.Id;
                worksheet.Cell(row, 2).Value = order.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");
                worksheet.Cell(row, 3).Value = order.TotalAmount;
                worksheet.Cell(row, 4).Value = order.Status.ToString();
                row++;
            }

            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var filePath = Path.Combine(desktopPath, $"User_{userId}_Orders.xlsx");

            workbook.SaveAs(filePath);

            return filePath;
        }
    }


}
