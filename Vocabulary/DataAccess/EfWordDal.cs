using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Models;

namespace Vocabulary.DataAccess
{
    public class EfWordDal
    {
        private readonly VocabularyContext _vocabularyContext;

        public EfWordDal(VocabularyContext vocabularyContext)
        {
            _vocabularyContext = vocabularyContext;
        }

        public List<Word> GetAll()
        {
            return _vocabularyContext.Words.ToList();
        }

        public List<Word> GetByWord(string key)
        {
            return _vocabularyContext.Words.Where(w => w.EnglishMeaning.ToLower().Contains(key.ToLower())||w.AzerbaijanMeaning.ToLower().Contains(key.ToLower())).ToList();
        }

        public Word GetById(int id)
        {
            return _vocabularyContext.Words.FirstOrDefault(w => w.Id == id);
        }

        public void Add(Word word)
        {
            var addedWord = _vocabularyContext.Entry(word);
            addedWord.State = EntityState.Added;
            _vocabularyContext.SaveChanges();
        }
        public void Delete(Word word)
        {
            var deletedWord = _vocabularyContext.Entry(word);
            deletedWord.State = EntityState.Deleted;
            _vocabularyContext.SaveChanges();
        }
    }
}
