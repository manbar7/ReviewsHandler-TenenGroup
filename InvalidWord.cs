using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewsHandler
{
    class InvalidWord
    {
        public int wordId { get; set; }
        public string word { get; set; }

        public InvalidWord()
        {
        }

        public override string ToString()
        {
            return $"ID:{wordId} | Invalid Word:{word}";
        }
    }
}
