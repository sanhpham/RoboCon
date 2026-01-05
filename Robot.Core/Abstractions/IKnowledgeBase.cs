using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Abstractions
{
    public interface IKnowledgeBase
    {
        void Ingest(IEnumerable<string> chunks);
        IEnumerable<string> Search(string query, int topK = 3);
    }
}
