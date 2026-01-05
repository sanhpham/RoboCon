using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.AI
{
    public class FakeAIService : IAIService
    {
        public Task<string> AskAsync(string prompt)
        {
            return Task.FromResult($"[AI MOCK] You said: {prompt}");
        }
    }
}
