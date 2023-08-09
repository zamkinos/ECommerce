using ConsoleECommerce.Objects;
using ConsoleECommerce.Operations;
using Constants = ConsoleECommerce.Constants;

Constants.OAuthUserToken = await LoginOperations.GetTokenAsync();

OrderOperations objOrderOperations = new OrderOperations();
GetOrdersResponse objGetOrdersResponse = await  objOrderOperations.GetOrdersAsync("2023-08-01T08:30:00.000");

foreach (Order o in objGetOrdersResponse.orders)
{
    Console.WriteLine("Order Id : " + o.orderId + " - Creation Date : " + o.creationDate.ToString("dd.MM.yyyy HH:mm:ss"));
}
Console.Read();
