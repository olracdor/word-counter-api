
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WordCounter.Bouncer;
using WordCounter.Bouncer.Impl;
using WordCounter.Commands;
using WordCounter.Models;
using WordCounter.Services;

namespace WordCounter.Controllers
{

    [ApiController]
    [Route("api/v1/words")]
    public class WordCounterController : ControllerBase
    {
        
        private readonly IMediator _mediator;
        private IGetWordsService _getWordsService;
        IBouncer<UrlRequest> _countWordsBouncer;
        public WordCounterController(IMediator mediator
            , IGetWordsService getWordsService
            , IBouncer<UrlRequest> countWordsBouncer)
        {
            _mediator = mediator;
            _getWordsService=getWordsService;
            _countWordsBouncer = countWordsBouncer;
        }

        [HttpGet]
        [Route("count")]
        public IActionResult GetWords()
        {
            var result = _getWordsService.GetWords();
            return Ok(result);
        }

        [HttpPost]
        [Route("count")]
        public async Task<IActionResult> CountWords([FromBody] UrlRequest urlRequest)
        {
            var bounce = await _countWordsBouncer.Bounce(urlRequest);
            if (!bounce.IsSuccessful)
            {
                return BadRequest(bounce.Message);
            }
            var result = await _mediator.Send(new WordCounterCommand { Url = urlRequest.url });
            return Ok(result);
        }
    }
}
