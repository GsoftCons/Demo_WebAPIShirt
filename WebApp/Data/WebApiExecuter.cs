namespace WebApp.Data
{
    public class WebApiExecuter : IWebApiExecuter
    {
        private readonly HttpClient httpClient;

        public WebApiExecuter(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T?> InvokeGet<T>(string relativeUrl)
        {
            return await httpClient.GetFromJsonAsync<T>(relativeUrl);
        }

        public async Task<T?> InvokePost<T>(string relativeUrl, T data)
        {
            var response = await httpClient.PostAsJsonAsync(relativeUrl, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task InvokePut<T>(string relativeUrl, T data)
        {
            var response = await httpClient.PutAsJsonAsync(relativeUrl, data);
            response.EnsureSuccessStatusCode();
        }

        public async Task InvokeDelete(string relativeUrl)
        {
            var response = await httpClient.DeleteAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
        }
    }
}
