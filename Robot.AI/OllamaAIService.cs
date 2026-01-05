using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Robot.AI
{
    public class OllamaAIService : IAIService
    {

        private readonly HttpClient _http = new()
        {
            BaseAddress = new Uri("http://localhost:11434")
        };

        private readonly string _model;

        public OllamaAIService(string model)
        {
            _model = model;
        }

        public async Task<string> AskAsync(string prompt)
        {
            var payload = new
            {
                model = _model,
                stream = false,
                messages = new[]
            {
                new { role = "user", content = prompt }
            }
            };

            var res = await _http.PostAsync(
                "/api/chat",
                new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var json = await res.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            return doc.RootElement
                .GetProperty("message")
                .GetProperty("content")
                .GetString()!;
        }
    }
}
