using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OmniShareUWP.Models;

namespace OmniShareUWP.Services
{
    public class SharedContentItemModelServices
    {

        private const string WebServiceUrl = "http://taskmodel.azurewebsites.net/api/TaskModels/";

        public async Task<List<SharedContenItemtModel>> GetTaskModelsAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(WebServiceUrl);

            var taskModels = JsonConvert.DeserializeObject<List<SharedContenItemtModel>>(json);

            return taskModels;
        }

        public async Task<bool> AddTaskModelAsync(SharedContenItemtModel taskModel)
        {
            var httpClient = new HttpClient();

            var json = JsonConvert.SerializeObject(taskModel);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(WebServiceUrl, httpContent);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskModelAsync(SharedContenItemtModel taskModel)
        {
            var httpClient = new HttpClient();

            var response = await httpClient.DeleteAsync(WebServiceUrl + taskModel.Id);

            return response.IsSuccessStatusCode;
        }
    }
}
