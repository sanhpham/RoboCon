using RoboCon.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Memory;
public class InMemoryService : IMemoryService
{
    private readonly List<string> _history = new();
    public Task<string> RecallAsync(string input)
    {
        return Task.FromResult(string.Join("\n", _history.TakeLast(5)));
    }

    public Task SaveAsync(string input, string output)
    {
        _history.Add($"User: {input}\nBot: {output}");
        return Task.CompletedTask;
    }
}
