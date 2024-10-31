using RESTFulSense.Clients;
using PersonalRecipeManagerWebApp.Models.Configurations;

namespace PersonalRecipeManagerWebApp.Brokers.Apis
{
    public partial class MealDbRecipeBroker : IMealDbRecipeBroker
    {
        private readonly IRESTFulApiFactoryClient apiClient;

        public MealDbRecipeBroker(IRESTFulApiFactoryClient apiClient, IConfiguration config)
        {
            //this.httpClient = httpClient;
            //this.apiClient = GetApiClient(config);
            this.apiClient = apiClient;
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        //private IRESTFulApiFactoryClient GetApiClient(IConfiguration config)
        //{
        //    LocalConfigurations localConfig =
        //        config.Get<LocalConfigurations>();

        //    string apiBaseUrl = localConfig.ApiConfigurations.Url;
        //    this.httpClient.BaseAddress = new Uri(apiBaseUrl);

        //    return new RESTFulApiFactoryClient(this.httpClient);
        //}
    }
}
