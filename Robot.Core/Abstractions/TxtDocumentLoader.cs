using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Abstractions
{
    public class TxtDocumentLoader : IDocumentLoader
    {
        public string Load(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
