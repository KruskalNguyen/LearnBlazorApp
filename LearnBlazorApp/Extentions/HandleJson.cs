using System.Net.Http;
using System.Text.Json;

namespace LearnBlazorApp.Extentions
{
    public class HandleJson
    {
        public List<string> keys;
        public List<List<string>> values;
        private HttpClient _httpClient;
        public HandleJson(HttpClient httpClient) { 
            keys = new List<string>();
            values = new List<List<string>>();
            _httpClient = httpClient;
        }
        public async Task getData(string api)
        {
            var responseStream = await _httpClient.GetStreamAsync(api);
            var usersJson = await JsonDocument.ParseAsync(responseStream);

            foreach (var element in usersJson.RootElement.EnumerateArray())
            {
                foreach (var property in element.EnumerateObject())
                {
                    keys.Add(property.Name);
                }
                break;
            }
            foreach (var element in usersJson.RootElement.EnumerateArray())
            {
                var value = new List<string>();
                foreach (var property in element.EnumerateObject())
                {
                    value.Add(property.Value.ToString());
                }
                values.Add(value);
            }
        }
    }
}
