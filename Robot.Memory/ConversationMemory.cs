using Robot.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Memory;
public class ConversationMemory : IConversationMemory
{
    private readonly List<(string Role, string Content)> _messages = new();

    public void AddUser(string text)
        => _messages.Add(("user", text));

    public void AddAssistant(string text)
        => _messages.Add(("assistant", text));

    public IReadOnlyList<(string Role, string Content)> GetAll()
        => _messages;

    public void Clear() => _messages.Clear();
}
