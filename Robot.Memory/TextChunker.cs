using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot.Memory
{
    public class TextChunker
    {
        public static IEnumerable<string> Chunk(
        string text,
        int maxLength = 500)
        {
            var words = text.Split(' ');
            var chunk = "";

            foreach (var word in words)
            {
                if ((chunk + word).Length > maxLength)
                {
                    yield return chunk.Trim();
                    chunk = "";
                }

                chunk += word + " ";
            }

            if (!string.IsNullOrWhiteSpace(chunk))
                yield return chunk.Trim();
        }
    }
}
