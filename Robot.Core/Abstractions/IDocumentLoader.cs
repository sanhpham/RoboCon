using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Abstractions
{
    public interface IDocumentLoader
    {
        string Load(string path);
    }
}
