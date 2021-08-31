using System;
using System.Threading.Tasks;

namespace MediatorFromScratch.Sample
{
    public class PrintToConsoleHandler : IHandler<PrintToConsoleRequest, bool>
    {
        public Task<bool> HandlerAsync(PrintToConsoleRequest request)
        {
            Console.WriteLine(request.Text);
            return Task.FromResult(true);
        }
    }
}
