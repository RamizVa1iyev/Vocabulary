using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Models;

namespace Vocabulary.DataAccess
{
    public class VocabularyContext:DbContext
    {
        public VocabularyContext(DbContextOptions<VocabularyContext> options):base(options)
        {
            
        }
        public DbSet<Word> Words { get; set; }
    }
}
