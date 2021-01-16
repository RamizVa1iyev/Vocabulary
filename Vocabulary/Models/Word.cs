using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string AzerbaijanMeaning { get; set; }
        public string EnglishMeaning { get; set; }
        public DateTime Time { get; set; }
    }
}
