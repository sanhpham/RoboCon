using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboCon.Abstractions
{
    public interface IAIService
    {
        Task<string> AskAsync(string prompt);
    }
}
