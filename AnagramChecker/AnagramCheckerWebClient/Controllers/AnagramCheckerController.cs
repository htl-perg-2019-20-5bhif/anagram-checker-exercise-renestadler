﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramChecker.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnagramChecker.Controllers
{
    [ApiController]
    public class AnagramCheckerController : ControllerBase
    {

        private readonly ILogger<AnagramCheckerController> _logger;

        public AnagramCheckerController(ILogger<AnagramCheckerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("/api/checkAnagram")]
        public ActionResult<string> GetCheckAnagrams([FromBody]WordCheckerDto data)
        {
            if (data is null)
            {
                return BadRequest();
            }
            AnagramChecker anagramChecker = new AnagramChecker();
            if (anagramChecker.CheckWords(data.w1, data.w2))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("/api/getKnownAnagrams")]
        public async Task<ActionResult<string>> GetKnownAnagramsAsync([FromQuery] string w)
        {
            if (w is null)
            {
                return BadRequest();
            }
            AnagramChecker anagramChecker = new AnagramChecker();
            IEnumerable<string> anagrams = await anagramChecker.GetKnownAnagramsAsync(w);
            if (anagrams.Count() == 0)
            {
                _logger.LogWarning("No anagrams found for " + w +
                    ". This must not nescessary mean that there are no anagrams, but none of them is included in our dictionary as anagram of the inputed word");
                return NotFound();
            }
            else
            {
                return Ok(anagrams);
            }
        }

        [HttpGet]
        [Route("/api/getPermutations")]
        public async Task<ActionResult<string>> GetPermutationsAsync([FromQuery] string w)
        {
            if (w is null)
            {
                return BadRequest();
            }
            AnagramChecker anagramChecker = new AnagramChecker();
            IEnumerable<string> anagrams = await anagramChecker.GetPermutationsAsync(w);
            if (anagrams.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(anagrams);
            }
        }
    }
}
