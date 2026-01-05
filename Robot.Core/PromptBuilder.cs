using Robot.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core
{
    public static class PromptBuilder
    {
        public static string Build(
        string systemPrompt,
        IConversationMemory shortMemory,
        ILongTermMemory longMemory,
        IEnumerable<string> knowledge)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"SYSTEM: {systemPrompt}");
            sb.AppendLine();

            if (knowledge.Any())
            {
                sb.AppendLine("KNOWLEDGE:");
                foreach (var k in knowledge)
                    sb.AppendLine($"- {k}");
                sb.AppendLine();
            }

            if (!string.IsNullOrWhiteSpace(longMemory.GetSummary()))
            {
                sb.AppendLine("LONG-TERM MEMORY:");
                sb.AppendLine(longMemory.GetSummary());
                sb.AppendLine();
            }

            foreach (var (role, content) in shortMemory.GetAll())
            {
                sb.AppendLine($"{role}: {content}");
            }

            sb.AppendLine("ASSISTANT:");
            return sb.ToString();
        }
    }
}
