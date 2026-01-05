using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Core.Models
{
    public record Message(string Role, string Content, DateTime Time);
    
}
