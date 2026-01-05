using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Robot.AI
{
    public class HuggingFaceService : IAIService
    {
        private readonly HttpClient _http;
        private readonly string _model;

        public HuggingFaceService(string apiKey, string model)
        {
            _model = model;
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://api-inference.huggingface.co/")
            };
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> AskAsync(string prompt)
        {
            var payload = new
            {
                inputs = prompt,
                parameters = new
                {
                    max_new_tokens = 200,
                    temperature = 0.7
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _http.PostAsync(
                $"models/{_model}",
                content
            );

            if (!response.IsSuccessStatusCode)
            {
                return $"[HF ERROR] {response.StatusCode}";
            }

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            return doc.RootElement[0]
                .GetProperty("generated_text")
                .GetString()!;
        }
    }
}
