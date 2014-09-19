using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.WordSeg
{
    [Serializable]
    internal class WordPosCollection
    {
        internal WordPosCollection()
        {
            this.WordPosList = new List<WordPos>();
        }
        internal List<WordPos> WordPosList { get; set; }
    }

}
