using Robot.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Memory
{
    public class LongTermMemory : ILongTermMemory
    {
        private string? _summary;

        public string? GetSummary() => _summary;

        public void SaveSummary(string summary)
            => _summary = summary;
    }
}
