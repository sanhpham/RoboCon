using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Abstractions
{
    public interface IConversationMemory
    {
        void AddUser(string text);
        void AddAssistant(string text);
        IReadOnlyList<(string Role, string Content)> GetAll();
        void Clear();
    }
}
