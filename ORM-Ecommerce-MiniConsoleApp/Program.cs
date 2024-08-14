UserService userService = new UserService();
ProductService productService = new ProductService();

UserPostDto user = new UserPostDto
{
    FullName = "Sabir Mehdi",
    Email = "sabirsm1@code.edu.az",
    Password = "password",
};
//await userService.CreateUserAsync(user);
//Console.WriteLine(await userService.GetUserByIdAsync());
//await userService.DeleteUserAsync(3);

UserPutDto userPutDto = new()
{
    Id = 3,
    Password = "Salam123"
};
//await userService.UpdateUserInfoAsync(userPutDto);

//var searchResults = await productService.SearchProductsAsync("s");

//foreach (var searchResult in searchResults)
//{
//    Console.WriteLine(searchResult);
//}

ProductPostDto productPostDto = new()
{
    Name = "Test",
    Price = 1
};
//await productService.AddProductAsync(productPostDto);


//var list = await productService.GetAllProductsAsync();
//foreach (var product in list)
//{
//    Console.WriteLine(product);
//}

ProductPutDto productPutDto = new()
{
    Id= 6,
    Name = "UpdProd",
    Stock = 11,
};

await productService.UpdateProductAsync(productPutDto);

