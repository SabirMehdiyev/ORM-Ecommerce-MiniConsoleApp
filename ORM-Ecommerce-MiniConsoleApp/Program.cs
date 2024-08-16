UserService userService = new UserService();
ProductService productService = new ProductService();
OrderService orderService = new OrderService();
PaymentService paymentService = new PaymentService();

User activeUser = null;

bool isApplicationRunning = true;

while (isApplicationRunning)
{
    if (activeUser == null)
    {
        await ShowLoginMenu();
    }
    else
    {
        await ShowUserMenu();
    }
}
async Task ShowLoginMenu()
{

    Console.WriteLine("Welcome!");

    bool loginProcess = true;
    while (loginProcess)
    {
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Exit");
        Console.WriteLine("Please select an option (1-3): ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await Register();
                break;
            case "2":
                await Login();
                if (activeUser != null)
                {
                    loginProcess = false;
                }
                break;
            case "3":
                Console.WriteLine("Exiting the application. Goodbye!");
                isApplicationRunning = false;
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
    }
}
async Task ShowUserMenu()
{
    bool menuRunning = true;

    while (menuRunning)
    {
        Console.Clear();
        Console.WriteLine($"Welcome {activeUser.FullName}");
        Console.WriteLine("Menu:");
        Console.WriteLine("1. Update User Info");
        Console.WriteLine("2. Get User Orders");
        Console.WriteLine("3. Export Orders to Excel");
        Console.WriteLine("4. Manage Products");
        Console.WriteLine("5. Manage Orders");
        Console.WriteLine("6. Manage Payments"); 
        Console.WriteLine("7. Logout");
        Console.Write("Please select an option (1-7): ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await UpdateUserInfo();
                break;
            case "2":
                await GetUserOrders();
                break;
            case "3":
                await ExportOrdersToExcel();
                break;
            case "4":
                await ManageProducts();
                break;
            case "5":
                await ManageOrders();
                break;
            case "6":
                await ManagePayments(); 
                break;
            case "7":
                activeUser = null;
                menuRunning = false;
                Console.WriteLine("You have been logged out.");
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                Console.ReadLine();
                break;
        }
    }
}
async Task ManageProducts()
{
    bool managingProducts = true;

    while (managingProducts)
    {
        Console.Clear();
        Console.WriteLine("Product Management Menu:");
        Console.WriteLine("1. Add Product");
        Console.WriteLine("2. Update Product");
        Console.WriteLine("3. Delete Product");
        Console.WriteLine("4. List All Products");
        Console.WriteLine("5. Search Products");
        Console.WriteLine("6. Return to Main Menu");
        Console.Write("Please select an option (1-6): ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await AddProduct();
                break;
            case "2":
                await UpdateProduct();
                break;
            case "3":
                await DeleteProduct();
                break;
            case "4":
                await ListAllProducts();
                break;
            case "5":
                await SearchProducts();
                break;
            case "6":
                managingProducts = false;
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
    }
}
async Task ManageOrders()
{
    bool managingOrders = true;

    while (managingOrders)
    {
        Console.Clear();
        Console.WriteLine("Order Management Menu:");
        Console.WriteLine("1. Create Order");
        Console.WriteLine("2. Cancel Order");
        Console.WriteLine("3. Complete Order");
        Console.WriteLine("4. View Order Details");
        Console.WriteLine("5. List Orders");
        Console.WriteLine("6. Return to Main Menu");
        Console.Write("Please select an option (1-6): ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await CreateOrder();
                break;
            case "2":
                await CancelOrder();
                break;
            case "3":
                await CompleteOrder();
                break;
            case "4":
                await ViewOrderDetails();
                break;
            case "5":
                await ListOrders();
                break;
            case "6":
                managingOrders = false;
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                break;
        }
        
    }
}
async Task ManagePayments()
{
    bool paymentMenuRunning = true;

    while (paymentMenuRunning)
    {
        Console.Clear();
        Console.WriteLine("Manage Payments");
        Console.WriteLine("1. View Payments");
        Console.WriteLine("2. Make Payment");
        Console.WriteLine("3. Back to Main Menu");
        Console.Write("Please select an option (1-3): ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ViewPayments();
                break;
            case "2":
                await MakePayment();
                break;
            case "3":
                paymentMenuRunning = false;
                break;
            default:
                Console.WriteLine("Invalid selection. Please try again.");
                Console.ReadLine();
                break;
        }
    }
}

async Task ViewPayments()
{
    var payments = await paymentService.GetPaymentsAsync(activeUser.Id);

    Console.Clear();
    Console.WriteLine("Your Payments:");
    foreach (var payment in payments)
    {
        Console.WriteLine($"PaymentID: {payment.Id}, Order ID: {payment.OrderId}, Amount: {payment.Amount}, Date: {payment.PaymentDate}");
    }
    Console.WriteLine("Press Enter to return to the payment menu.");
    Console.ReadLine();
}

async Task MakePayment()
{
    var orders = await orderService.GetOrdersAsync(activeUser.Id);

    Console.Clear();
    Console.WriteLine("Your Orders:");
    if (orders.Count == 0)
    {
        Console.WriteLine("No orders found.");
        Console.WriteLine("Press Enter to return to the payment menu.");
        Console.ReadLine();
        return;
    }

    foreach (var order in orders)
    {
        Console.WriteLine($"Order ID: {order.Id}, Date: {order.OrderDate}, Total Amount: {order.TotalAmount}, Status: {order.Status}");
    }

    Console.Write("Enter Order ID to make a payment: ");
    if (!int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("Invalid Order ID.");
        Console.WriteLine("Press Enter to return to the payment menu.");
        Console.ReadLine();
        return;
    }
    Console.Write("Enter payment amount to make a payment: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
    {
        Console.WriteLine("Invalid payment amount. It must be greater than zero.");
        Console.WriteLine("Press Enter to return to the payment menu.");
        Console.ReadLine();
        return;
    }

    var paymentDTO = new PaymentPostDto
    {
        OrderId = orderId,
        Amount = amount
    };

    try
    {
        await paymentService.MakePaymentAsync(paymentDTO);
        Console.WriteLine("Payment successfully processed.");
    }
    catch (NotFoundException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    catch (InvalidPaymentException ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("Press Enter to return to the payment menu.");
    Console.ReadLine();
}

async Task Register()
{
    Console.Clear();
    Console.WriteLine("Register a new user");

    while (true)
    {
        Console.Write("Please enter FullName: ");
        string fullName = Console.ReadLine();

        Console.Write("Please enter Email: ");
        string email = Console.ReadLine();

        Console.Write("Please enter Password: ");
        string password = Console.ReadLine();

        Console.Write("Please enter Address: ");
        string address = Console.ReadLine();

        try
        {
            var newUser = new UserPostDto
            {
                FullName = fullName,
                Email = email,
                Password = password,
                Address = address
            };

            await userService.RegisterUserAsync(newUser);
            Console.WriteLine("User registered successfully!");
            return;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
async Task Login()
{
    Console.Clear();
    Console.WriteLine("Login");

    while (true)
    {
        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        try
        {
            var activeUserDto = await userService.LoginAsync(email, password);
            activeUser = new User
            {
                Id = activeUserDto.Id,
                FullName = activeUserDto.FullName,
                Email = activeUserDto.Email,
                Address = activeUserDto.Address
            };
            Console.WriteLine("Login successful!");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
async Task CreateOrder()
{
    Console.Clear();
    Console.WriteLine("Create New Order");

    var orderDetails = new List<OrderDetailPostDto>();
    decimal totalAmount = 0m;
    bool addingDetails = true;

    while (addingDetails)
    {
        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid Product ID. Please try again.");
            continue;
        }

        Console.Write("Enter Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid Quantity. Please try again.");
            continue;
        }

        try
        {
            var product = await productService.GetProductByIdAsync(productId);  
            if (product == null)
            {
                Console.WriteLine("Product not found. Please try again.");
                continue;
            }

            decimal pricePerItem = product.Price;  
            decimal detailTotalAmount = pricePerItem * quantity;
            totalAmount += detailTotalAmount;
            
            orderDetails.Add(new OrderDetailPostDto
            {
                ProductId = productId,
                Quantity = quantity,
                PricePerItem = pricePerItem
            });

            Console.Write("Add another product? (y/n): ");
            if (Console.ReadLine().ToLower() != "y")
            {
                addingDetails = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    if (!orderDetails.Any())
    {
        Console.WriteLine("No products added. Order creation aborted.");
        return;
    }

    var orderDto = new OrderPostDto
    {
        UserId = activeUser.Id,
        OrderDetails = orderDetails,
        TotalAmount = totalAmount
    };

    try
    {
        await orderService.CreateOrderAsync(orderDto);
        Console.WriteLine("Order created successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error creating order: {ex.Message}");
    }

    Console.WriteLine("Press Enter to return to the menu...");
    Console.ReadLine();
}
async Task CancelOrder()
{
    Console.Clear();
    Console.WriteLine("Cancel Order");

    Console.Write("Enter Order ID: ");
    if (!int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("Invalid Order ID.");
        return;
    }

    try
    {
        await orderService.CancelOrderAsync(orderId);
        Console.WriteLine("Order cancelled successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}
async Task CompleteOrder()
{
    Console.Clear();
    Console.WriteLine("Complete Order");

    Console.Write("Enter Order ID: ");
    if (!int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("Invalid Order ID.");
        return;
    }

    try
    {
        await orderService.CompleteOrderAsync(orderId);
        Console.WriteLine("Order completed successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}
async Task ViewOrderDetails()
{
    Console.Clear();
    Console.WriteLine("View Order Details");

    Console.Write("Enter Order ID: ");
    if (!int.TryParse(Console.ReadLine(), out int orderId))
    {
        Console.WriteLine("Invalid Order ID.");
        return;
    }

    try
    {
        var orderDetails = await orderService.GetOrderDetailsByOrderIdAsync(orderId);
        Console.WriteLine("Order Details:");
        foreach (var detail in orderDetails)
        {
            Console.WriteLine($"OrderID: {detail.OrderId}, Product ID: {detail.ProductId}, Quantity: {detail.Quantity}, PricePerItem: {detail.PricePerItem}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}
async Task ListOrders()
{
    Console.Clear();
    Console.WriteLine("List Orders");

    try
    {
        var orders = await orderService.GetOrdersAsync(activeUser.Id);
        Console.WriteLine("Orders:");
        foreach (var order in orders)
        {
            Console.WriteLine($"ID: {order.Id}, Date: {order.OrderDate}, Total Amount: {order.TotalAmount}, Status: {order.Status}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}

async Task UpdateUserInfo()
{
    Console.Clear();
    Console.WriteLine("Update User Information");

    Console.Write("New Full Name (leave empty to keep current): ");
    string fullName = Console.ReadLine();

    Console.Write("New Email (leave empty to keep current): ");
    string email = Console.ReadLine();

    Console.Write("New Password (leave empty to keep current): ");
    string password = Console.ReadLine();

    Console.Write("New Address (leave empty to keep current): ");
    string address = Console.ReadLine();

    try
    {
        var userPutDto = new UserPutDto
        {
            Id = activeUser.Id,
            FullName = string.IsNullOrEmpty(fullName) ? null : fullName,
            Email = string.IsNullOrEmpty(email) ? null : email,
            Password = string.IsNullOrEmpty(password) ? null : password,
            Address = string.IsNullOrEmpty(address) ? null : address
        };

        await userService.UpdateUserInfoAsync(userPutDto);
        Console.WriteLine("User information updated successfully!");
        if (userPutDto.FullName != null) activeUser.FullName = userPutDto.FullName;
        if (userPutDto.Email != null) activeUser.Email = userPutDto.Email;
        if (userPutDto.Address != null) activeUser.Address = userPutDto.Address;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

async Task GetUserOrders()
{
    Console.Clear();
    Console.WriteLine("Fetching User Orders...");

    try
    {
        var orders = await userService.GetUserOrdersAsync(activeUser.Id);

        Console.WriteLine($"User Orders");
        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found.");
        }
        else
        {
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Total Amount: {order.TotalAmount}, Status: {order.Status}");
            }
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.ReadLine();
}

async Task ExportOrdersToExcel()
{
    Console.Clear();
    Console.WriteLine("Exporting Orders to Excel...");

    try
    {
        string filePath = await userService.ExportUserOrdersToExcel(activeUser.Id);
        Console.WriteLine($"User orders exported successfully to: {filePath}");

        Console.WriteLine("\nPress Enter to return to the menu...");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    Console.ReadLine();
}

async Task AddProduct()
{
    Console.Clear();
    Console.WriteLine("Add New Product");

    Console.Write("Product Name: ");
    string name = Console.ReadLine();

repeatPrice:
    decimal price;
    if (!IsValidPrice(out price))
    {
        Console.WriteLine("Please enter correct price");
        goto repeatPrice;
    }

repeatStock:
    Console.Write("Product Stock: ");
    string stockInput = Console.ReadLine();
    if (!int.TryParse(stockInput, out int stock))
    {
        Console.WriteLine("Please enter correct stock value");
        goto repeatStock;
    }

    Console.WriteLine("Product Description (optional): ");
    string description = Console.ReadLine();

    try
    {
        var newProduct = new ProductPostDto
        {
            Name = name,
            Price = price,
            Stock = stock,
            Description = description
        };

        await productService.AddProductAsync(newProduct);
        Console.WriteLine("Product added successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}

async Task UpdateProduct()
{
    Console.Clear();
    Console.WriteLine("Update Product");

repeatId:
    int id;
    if (!IsValidId(out id))
    {
        Console.WriteLine("Please enter correct Id");
        goto repeatId;
    }

    Console.Write("New Product Name (leave empty to keep current): ");
    string name = Console.ReadLine();

    decimal? price = null;
    Console.Write("New Product Price (leave empty to keep current): ");
    string priceInput = Console.ReadLine();
    if (!string.IsNullOrEmpty(priceInput))
    {
        while (!decimal.TryParse(priceInput, out decimal parsedPrice) || parsedPrice <= 0)
        {
            Console.WriteLine("Please enter a valid price greater than zero");
            priceInput = Console.ReadLine();
        }
        price = decimal.Parse(priceInput);
    }


    int? stock = null;
    Console.Write("New Product Stock (leave empty to keep current): ");
    string stockInput = Console.ReadLine();
    if (!string.IsNullOrEmpty(stockInput))
    {
        while (!int.TryParse(stockInput, out int parsedStock) || parsedStock < 0)
        {
            Console.WriteLine("Please enter a valid non-negative stock value");
            stockInput = Console.ReadLine();
        }
        stock = int.Parse(stockInput);
    }
    else
    {
        stock = null;
    }

    Console.Write("New Product Description (leave empty to keep current): ");
    string description = Console.ReadLine();

    try
    {
        var productPutDto = new ProductPutDto
        {
            Id = id,
            Name = string.IsNullOrEmpty(name) ? null : name,
            Price = price,
            Stock = stock,
            Description = string.IsNullOrEmpty(description) ? null : description
        };
        await productService.UpdateProductAsync(productPutDto);
        Console.WriteLine("Product updated successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}
async Task DeleteProduct()
{
    Console.Clear();
    Console.WriteLine("Delete Product");
repeatId:

    Console.Write("Product ID: ");

    int id;
    if (!IsValidId(out id))
    {
        Console.WriteLine("Please enter correct Id");
        goto repeatId;
    }

    try
    {
        await productService.DeleteProductAsync(id);
        Console.WriteLine("Product deleted successfully!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}

async Task ListAllProducts()
{
    Console.Clear();
    Console.WriteLine("List All Products");

    try
    {
        var products = await productService.GetAllProductsAsync();

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}

async Task SearchProducts()
{
    Console.Clear();
    Console.WriteLine("Search Products");

    Console.Write("Search Query: ");
    string query = Console.ReadLine();

    try
    {
        var products = await productService.SearchProductsAsync(query);

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Stock: {product.Stock}");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }

    Console.WriteLine("\nPress Enter to return to the menu...");
    Console.ReadLine();
}
static bool IsValidId(out int id)
{
    Console.Write("Product ID: ");
    return int.TryParse(Console.ReadLine(), out id) && id > 0;
}

static bool IsValidPrice(out decimal price)
{
    Console.Write("Product Price: ");
    return decimal.TryParse(Console.ReadLine(), out price);
}

