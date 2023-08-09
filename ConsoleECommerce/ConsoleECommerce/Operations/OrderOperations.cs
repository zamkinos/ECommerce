using ConsoleECommerce.Objects;
using Newtonsoft.Json;

namespace ConsoleECommerce.Operations
{
    internal class OrderOperations
    {
        internal GetOrdersResponse GetOrders(string dt)
        {
            string p = null;
            BasicResponse res = ApiOperations.SendRequestWithBearerToken("fulfillment/v1/order?filter=creationdate:%5B" + dt + "Z..%5D", p);
            if (res.HasError)
            {
                // Log the error later
                return new GetOrdersResponse();
            }
            else
            {
                return JsonConvert.DeserializeObject<GetOrdersResponse>(res.JsonResult);
            }
        }
    }
}
