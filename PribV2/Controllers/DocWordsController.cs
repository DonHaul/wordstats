using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PribV2.Models;

//for debug purposes
using System.Diagnostics;

namespace PribV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocWordsController : ControllerBase
    {
        private readonly DocWordContext _context;

        public DocWordsController(DocWordContext context)
        {
            _context = context;
        }

        // GET: api/DocWords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocWord>>> GetTodoItems()
        {
            return await _context.DocWords.ToListAsync();
        }

        // GET: api/DocWords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocWord>> GetDocWord(long id)
        {
            var docWord = await _context.DocWords.FindAsync(id);

            if (docWord == null)
            {
                return NotFound();
            }

            return docWord;
        }


        //THIS IS THE PUT DOCS ROUTE
        // PUT: api/DocWords/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutDocWord(Document doc)
        {
            //post snippet
            /*
                         _context.TodoItems.Add(docWord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocWord", new { id = docWord.Id }, docWord);
             */


            //must have a body
            if (doc.body == null)
            {
                return BadRequest();
            }

            //gets array
            string[] words = doc.body.Split(' ');

            //get counts TODO put this as a function in another file
            //use linq or by hand
            //https://stackoverflow.com/questions/1139181/a-method-to-count-occurrences-in-a-list
            var g = words.GroupBy(i => i);
            foreach (var grp in g)
            {
                Debug.WriteLine("{0} {1}", grp.Key, grp.Count());
                //UPDATE DB

                Debug.WriteLine(grp.Key.GetType());

                string lol = grp.Key;

                DocWord docWord = (from P in _context.DocWords.Where(b => b.Name == grp.Key) select P).FirstOrDefault();


                
                if(docWord==null)
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

                await _context.SaveChangesAsync();
                return NoContent();
                /*
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocWordExistsWord(grp.Key))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }


            }

            Debug.WriteLine(g);
            Debug.WriteLine(words);

            
            return Ok();*/

            //PREVIOUS PUT CODE
            /*
            if (id != docWord.Id)
            {
                return BadRequest();
            }

            _context.Entry(docWord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocWordExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/DocWords
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DocWord>> PostDocWord(DocWord docWord)
        {
            _context.DocWords.Add(docWord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocWord", new { id = docWord.Id }, docWord);
        }

        // DELETE: api/DocWords/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocWord>> DeleteDocWord(long id)
        {
            var docWord = await _context.DocWords.FindAsync(id);
            if (docWord == null)
            {
                return NotFound();
            }

            _context.DocWords.Remove(docWord);
            await _context.SaveChangesAsync();

            return docWord;
        }

        private bool DocWordExists(long id)
        {
            return _context.DocWords.Any(e => e.Id == id);
        }

        private bool DocWordExistsWord(string word)
        {
            return _context.DocWords.Any(e => e.Name == word);
        }
    }
}
