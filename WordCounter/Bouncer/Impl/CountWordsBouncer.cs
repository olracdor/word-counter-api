using System;
using System.Threading.Tasks;
using WordCounter.Models;

namespace WordCounter.Bouncer.Impl
{
    public class CountWordsBouncer : IBouncer<UrlRequest>
    {
        public CountWordsBouncer()
        {
        }

        public async Task<BouncerResult> Bounce(UrlRequest request)
        {
            try
            {
                var url = new Uri(request.url);
                return new BouncerResult { IsSuccessful = true, Message = request.url };
            }
            catch(Exception ex)
            {
                return new BouncerResult { IsSuccessful=false, Message=ex.Message };
            }

        }
    }
}
