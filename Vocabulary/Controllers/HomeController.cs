using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Vocabulary.DataAccess;
using Vocabulary.Models;

namespace Vocabulary.Controllers
{
    public class HomeController : Controller
    {

        private readonly VocabularyContext _vocabularyContext;
        private EfWordDal _efWordDal;
        public HomeController(VocabularyContext vocabularyContext)
        {
            _vocabularyContext = vocabularyContext;
            _efWordDal = new EfWordDal(_vocabularyContext);
        }

        public IActionResult Index(string key)
        {
            if (key == null) key = "";
            string sortType = Request.Query["sortType"];
            var efWordDal = new EfWordDal(_vocabularyContext);
            var words = efWordDal.GetByWord(key);
            var model = new HomeIndexViewModel()
            {
                Words = SortWords(words, sortType)
            };
            return View(model);
        }

        private List<Word> SortWords(List<Word> words, string sortType)
        {
            if (sortType == null) sortType = "AzerbaijanAtoZ";

            switch (sortType)
            {
                case "date":
                    return words.OrderBy(w => w.Time).ToList();
                case "AzerbaijanAtoZ":
                    return words.OrderBy(w => w.AzerbaijanMeaning).ToList();
                case "AzerbaijanZtoA":
                    return words.OrderBy(w => w.AzerbaijanMeaning).Reverse().ToList();
                case "EnglishAtoZ":
                    return words.OrderBy(w => w.EnglishMeaning).ToList();
                case "EnglishZtoA":
                    return words.OrderBy(w => w.EnglishMeaning).Reverse().ToList();
                default:
                    return words;
            }
        }

        [HttpPost]
        public IActionResult AddWord(Word word)
        {
            word.Time = DateTime.Now;
            _efWordDal.Add(word);
            return RedirectToAction("Index");
        }
        public IActionResult AddWord()
        {
            return View();
        }

        public IActionResult DeleteWord()
        {
            int id = int.Parse(Request.Query["wordId"]);
            var word = _efWordDal.GetById(id);
            _efWordDal.Delete(word);
            return RedirectToAction("Index");
        }
    }
}
