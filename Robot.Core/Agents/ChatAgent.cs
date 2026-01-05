using RoboCon.Abstractions;
using Robot.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Agents
{
    public class ChatAgent
    {
        private readonly IAIService _ai;
        private readonly IConversationMemory _shortMemory;
        private readonly ILongTermMemory _longMemory;
        private readonly SummaryAgent _summaryAgent;
        private readonly IKnowledgeBase _knowledgeBase;

        private const int MAX_MESSAGES = 10;

        private const string SystemPrompt =
            "You are a helpful AI assistant. Answer clearly and concisely.";

        public ChatAgent(
            IAIService ai,
            IConversationMemory shortMemory,
            ILongTermMemory longMemory,
            IKnowledgeBase knowledgeBase)
        {
            _ai = ai;
            _shortMemory = shortMemory;
            _longMemory = longMemory;
            _summaryAgent = new SummaryAgent(ai);
            _knowledgeBase = knowledgeBase;
        }

        public async Task<string> AskAsync(string userInput)
        {
            _shortMemory.AddUser(userInput);

            // 2️⃣ RAG: tìm kiến thức liên quan
            var knowledge = _knowledgeBase.Search(userInput);

            // 🔁 Nếu hội thoại dài → tóm tắt
            if (_shortMemory.GetAll().Count > MAX_MESSAGES)
            {
                var convoText = BuildConversationText();
                var summary = await _summaryAgent.SummarizeAsync(convoText);

                _longMemory.SaveSummary(summary);
                _shortMemory.Clear();
            }

            var prompt = PromptBuilder.Build(
                SystemPrompt,
                _shortMemory,
                _longMemory,
                knowledge
                );

            var answer = await _ai.AskAsync(prompt);

            _shortMemory.AddAssistant(answer);
            return answer;
        }

        private string BuildConversationText()
        {
            var sb = new StringBuilder();
            foreach (var (role, content) in _shortMemory.GetAll())
            {
                sb.AppendLine($"{role}: {content}");
            }
            return sb.ToString();
        }
    }
}
