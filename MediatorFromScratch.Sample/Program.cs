using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatorFromScratch.DependencyInjection;

namespace MediatorFromScratch.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                //.AddTransient<PrintToConsoleHandler>()
                .AddMediator(ServiceLifetime.Scoped, typeof(Program))
                .BuildServiceProvider();

            /*var handlerDetails = new Dictionary<Type, Type>
            {
                {typeof(PrintToConsoleRequest), typeof(PrintToConsoleHandler) }
            };*/

            //IMediator mediator = new Mediator(serviceProvider.GetRequiredService, handlerDetails);
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            var request = new PrintToConsoleRequest
            {
                Text = "Hello from Mediator"
            };

            await mediator.SendAsync(request);

            var resutlt = await mediator.SendAsync(new GiveMeAValueRequest());
            Console.WriteLine(resutlt);
        }
    }
}
