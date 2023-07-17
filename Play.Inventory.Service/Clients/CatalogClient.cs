using Play.Inventory.Service.DTOs;

namespace Play.Inventory.Service.Clients
{
    public class CatalogClient
    {
        private readonly HttpClient _httpClient;

        public CatalogClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<ReadCatalogItemDto>?> GetCatalogItemsAsync() =>
            await _httpClient.GetFromJsonAsync<IReadOnlyCollection<ReadCatalogItemDto>>("itens");
    }
}
