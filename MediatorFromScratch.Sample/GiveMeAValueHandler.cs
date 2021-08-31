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
