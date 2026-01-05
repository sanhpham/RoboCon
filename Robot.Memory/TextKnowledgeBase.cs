using Robot.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Memory
{
    public class TextKnowledgeBase : IKnowledgeBase
    {
        private readonly List<string> _chunks = new();

        public void Ingest(IEnumerable<string> chunks)
        {
            _chunks.AddRange(chunks);
        }

        public IEnumerable<string> Search(string query, int topK = 3)
        {
            return _chunks
                .Where(c => c.Contains(query,
                    StringComparison.OrdinalIgnoreCase))
                .Take(topK);
        }
    }
}
