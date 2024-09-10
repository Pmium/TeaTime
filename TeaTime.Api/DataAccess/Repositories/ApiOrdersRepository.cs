using System.Security.Policy;
using System.Text;
using System.Text.Json;
using TeaTime.Api.Domain.Orders;
using TeaTime.Api.Domain.Stores;


namespace TeaTime.Api.DataAccess.Repositories
{
    public class ApiOrdersRepository : IOrdersRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        private static readonly JsonSerializerOptions _propertyNameCaseInsensitive = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiOrdersRepository(IConfiguration configuration, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://cghhedutest1.cgmh.org.tw/teatime/api";

            // 增加 header
            var tokenHeader = "X-Api-Secret-Token";
            var secretToken = "20240909teatime90904202";
            httpClient.DefaultRequestHeaders.Add(tokenHeader, secretToken);
        }
        public Order AddOrderAndReturn(long storeId, OrderForCreation newOrder)
        {
            var json = JsonSerializer.Serialize(newOrder);
            
            var content = new StringContent(json, Encoding.UTF8, "application/json");
        

            var response = _httpClient.PostAsync($"{_baseUrl}/stores/{storeId}/orders", content).Result;
            response.EnsureSuccessStatusCode();

            var responseContent = response.Content.ReadAsStringAsync().Result;
            var order = JsonSerializer.Deserialize<Order>(responseContent, _propertyNameCaseInsensitive);
            return order!;
        }

        public Order? GetOrder(long storeId, long id)
        {
            var response = _httpClient.GetAsync($"{_baseUrl}/orders/{id}").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var order = JsonSerializer.Deserialize<Order>(content, _propertyNameCaseInsensitive);

            return order;
        }

        public IEnumerable<Order> GetOrders(long storeId)
        {
            var response = _httpClient.GetAsync($"{_baseUrl}/stores/{storeId}/orders").Result;
            response.EnsureSuccessStatusCode();

            var content = response.Content.ReadAsStringAsync().Result;
            var order = JsonSerializer.Deserialize<IEnumerable<Order>>(content, _propertyNameCaseInsensitive);

            if (order is not null)
            {
                return order;
            }

            return Enumerable.Empty<Order>();
        }

        internal static Task GetOrdersAsync(long storeId)
        {
            throw new NotImplementedException();
        }
    }
}
