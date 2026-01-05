using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Abstractions
{
    public class RobotBrain : IRobotBrain
    {
        private readonly IAIService _ai;
        private readonly IMemoryService _memory;
        public RobotBrain(IAIService ai, IMemoryService memory)
        {
            _ai = ai;
            _memory = memory;
        }
        public async Task<string> ProcessAsync(string input)
        {
            var context = await _memory.RecallAsync(input);
            var prompt = $"{context}\nUser: {input}";
            var answer = await _ai.AskAsync(prompt);
            await _memory.SaveAsync(input, answer);
            return answer;
        }
    }
}
