using OpenAI.Chat;
using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAI;

namespace Robot.AI
{
    public class OpenAIService : IAIService
    {
        private readonly ChatClient _client;
        private readonly string _model;

        public OpenAIService(string apiKey, string model)
        {
            _client = new ChatClient(model, apiKey);
            _model = model;
        }
        public async Task<string> AskAsync(string prompt)
        {
            var messages = new List<ChatMessage>
            {
                ChatMessage.CreateSystemMessage(
                    "You are a helpful robot assistant."
                ),
                ChatMessage.CreateUserMessage(prompt)
            };

            var response = await _client.CompleteChatAsync(messages);
            return response.Value.Content[0].Text;
        }
    }
}
