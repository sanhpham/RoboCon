using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Agents
{
    public class SummaryAgent
    {
        private readonly IAIService _ai;

        public SummaryAgent(IAIService ai)
        {
            _ai = ai;
        }

        public async Task<string> SummarizeAsync(string conversation)
        {
            var prompt = $"""
            Summarize the following conversation briefly.
            Keep important facts, preferences, and decisions.

            Conversation:
            {conversation}
            """;
            return await _ai.AskAsync(prompt);
        }
    }
}
