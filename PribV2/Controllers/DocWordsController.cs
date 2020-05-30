using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PribV2.Models;

using Newtonsoft.Json; //Used to convert Dictionaries to json
using System.Text.RegularExpressions;
using System;

namespace PribV2.Controllers
{
    /// <summary>
    /// Controll all the actions performed on the database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DocWordsController : ControllerBase
    {

        //DBcontext
        private readonly DocWordContext _context;

        public DocWordsController(DocWordContext context)
        {
            _context = context;
        }

        //used for testing
        // GET: api/DocWords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocWord>>> GetTodoItems()
        {
            //return all words on the db
            return await _context.DocWords.ToListAsync();
        }

        /// <summary>
        /// Get the word statistics for the json in the body and retrieve some of the previously stored stats
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/api/document/statistics")]
        public ActionResult<DocStats> PostDocWord(Document doc)
        {
            //group all equal words
            var wordGroups = GroupWords(doc);

            //class to return as json
            DocStats stats = new DocStats();

            //fetch document count from the database
            stats.DocumentCount =  (from P in _context.Globals.Where(b => b.Name == "DocCount") select P.Value).FirstOrDefault();

            //initialize dict        
            stats.Statistics = new Dictionary<string, WordStats>();

            //For each unique word found, 
            foreach (var grp in wordGroups)
            {
                //find occurences of that word on the database from previous documents
                int docWordTotal = (from P in _context.DocWords.Where(b => b.Name == grp.Key) select P.Count).FirstOrDefault();

                //add to the dict, the word itself, and an obj with occurrences on the received doc, as well as on previous documents
                stats.Statistics.Add(grp.Key, new WordStats(grp.Count(), docWordTotal));

            }

            //return the stats as a Json (see DocStats.cs) for structure
            return Content(JsonConvert.SerializeObject(stats, Formatting.None));
        }

        //THIS IS THE PUT DOCS ROUTE
        // PUT: api/DocWords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        [Route("/api/document")]
        public async Task<IActionResult> PutDocWord(Document doc)
        {

            //must have a body
            if (doc.body == null || doc.title == null)
            {
                return BadRequest();
            }


            //group equal words
            var wordsGroup = GroupWords(doc);

            //for every unique word found
            foreach (var grp in wordsGroup)
            {
                //fetch that word stats from the DB if exists
                DocWord docWord = (from P in _context.DocWords.Where(b => b.Name == grp.Key) select P).FirstOrDefault();

                
                if (docWord == null)
                {
                    //create new entry
                    docWord = new DocWord(grp.Key, grp.Count());
                    _context.DocWords.Add(docWord);
                }
                else
                {
                    //update count
                    docWord.Count += grp.Count();
                    _context.Entry(docWord).State = EntityState.Modified;
                }
            }

            //fetch and increment document count on the database
            GlobalVar doccount = (from P in _context.Globals.Where(b => b.Name == "DocCount") select P).FirstOrDefault();
            doccount.Value++;
            _context.Entry(doccount).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Catalogs all words found on the documaent
        /// </summary>
        /// <param name="doc">Document with words</param>
        /// <returns>IEnumerable with same words grouped</returns>
        IEnumerable<IGrouping<string, string>> GroupWords(Document doc)
        {
            //Concatenate title and body            
            string fullStr = doc.title + "\n" + doc.body;
            
            //1) Remove punctuatio and and other symbols, 2)convert to lowercase
            fullStr = RemoveSpecialCharacters(fullStr).ToLower();



            //get word list, split by spacings and paragraphs
            string[] words = fullStr.Split(new string[] { " ","\n","\t","\r" }, StringSplitOptions.RemoveEmptyEntries);

            //get word group by them self, which makes it easir to find ocurrences later
            //https://stackoverflow.com/questions/1139181/a-method-to-count-occurrences-in-a-list
            return words.GroupBy(i => i);
        }




        /// <summary>
        /// Remove any characters that are not letters used on the portuguese alphabet and numbers
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex("(?:[^a-z0-9áéíóúâêîôãõçàèìòù \n])", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, string.Empty);
        }
    }
   
}
