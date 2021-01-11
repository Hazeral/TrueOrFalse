using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TrueOrFalse
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<Response> GetQuestions(string query)
        {
            Response questionsResp = null;
            try
            {
                HttpResponseMessage response = await _client.GetAsync(query);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    questionsResp = JsonConvert.DeserializeObject<Response>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }

            return questionsResp;
        }
    }

    [Serializable]
    public class Response
    {
        [JsonProperty("response_code")]
        public short response_code { get; set; }

        [JsonProperty("results")]
        public Question[] results { get; set; }
    }
}