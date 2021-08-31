using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorFromScratch.Sample
{
    public class GiveMeAValueHandler : IHandler<GiveMeAValueRequest, string>
    {
        public Task<string> HandlerAsync(GiveMeAValueRequest request)
        {
            return Task.FromResult("Hello from Custom Mediator");
        }
    }
}
