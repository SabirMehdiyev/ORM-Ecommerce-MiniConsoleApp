using ORM_Ecommerce_MiniConsoleApp.Models;
using ORM_Ecommerce_MiniConsoleApp.Repositories.Implementations.Generic;
using ORM_Ecommerce_MiniConsoleApp.Repositories.Interfaces;

namespace ORM_Ecommerce_MiniConsoleApp.Repositories.Implementations;

public class OrderRepository:Repository<Order>,IOrderRepository
{
}
