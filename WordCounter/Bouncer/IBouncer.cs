using System;
using System.Threading.Tasks;
using WordCounter.Models;

namespace WordCounter.Bouncer
{
    public interface IBouncer<in T>
    {
        Task<BouncerResult> Bounce(T request);
    }
}
