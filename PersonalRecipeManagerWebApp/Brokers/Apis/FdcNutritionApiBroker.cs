using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using RESTFulSense.Clients;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial class FdcNutritionApiBroker : IFdcNutritionApiBroker
    {
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;

        public FdcNutritionApiBroker(IRESTFulApiFactoryClient apiClient, IConfiguration config)
        {
            string apikey = config.GetValue<string>("fdc_nutrient_api_key");
            this.apiClient = apiClient;
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);


        //private async ValueTask<T> GetAsync<T>(string relativeUrl)
        //{
        //    var response = await this.httpClient.GetStringAsync(relativeUrl);
        //    return JsonSerializer.Deserialize<T>(response);
        //}

        //private async ValueTask<T> PostAsync<T>(string relativeUrl, T content)
        //{
        //    var jsonContent = JsonSerializer.Serialize(content);
        //    var httpContent = new StringContent(jsonContent);

        //    var response = await httpClient.PostAsync(relativeUrl, httpContent);
        //    response.EnsureSuccessStatusCode();

        //    var responseString = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<T>(responseString);
        //}

        //private async ValueTask<T> PutAsync<T>(string relativeUrl, T content)
        //{
        //    var jsonContent = JsonSerializer.Serialize(content);
        //    var httpContent = new StringContent(jsonContent);

        //    var response = await httpClient.PutAsync(relativeUrl, httpContent);
        //    response.EnsureSuccessStatusCode();

        //    var responseString = await response.Content.ReadAsStringAsync();
        //    return JsonSerializer.Deserialize<T>(responseString);
        //}

        //private async ValueTask DeleteAsync(string relativeUrl)
        //{
        //    var response = await httpClient.DeleteAsync(relativeUrl);
        //    response.EnsureSuccessStatusCode();
        //}
    }
}
