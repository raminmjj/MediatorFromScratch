using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediatorFromScratch.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection AddMediator(
            this IServiceCollection services,
            ServiceLifetime lifetime,
            params Type[] markers)
        {
            var handlerInfo = new Dictionary<Type, Type>();

            foreach (var marker in markers)
            {
                var assmebly = marker.Assembly;
                var requests = GetClassesImplementingInterface(assmebly, typeof(IRequest<>));
                var handlers = GetClassesImplementingInterface(assmebly, typeof(IHandler<,>));

                requests.ForEach(x =>
                {
                    handlerInfo[x] =
                    handlers.SingleOrDefault(xx => x == xx.GetInterface("IHandler`2")!
                    .GetGenericArguments()[0]);
                }
                );

                var serviceDescriptor = handlers.Select(x =>
                new ServiceDescriptor(x, x, lifetime));
                services.TryAdd(serviceDescriptor);
            }

            services.AddSingleton<IMediator>(x => new Mediator(x.GetRequiredService, handlerInfo));

            return services;
        }

        private static List<Type> GetClassesImplementingInterface(System.Reflection.Assembly assmebly, Type typeToMatch)
        {
            var requests = assmebly.ExportedTypes
                .Where(type =>
                {
                    var genericInterfaceTypes = type.GetInterfaces()
                    .Where(x => x.IsGenericType);
                    var implementRequestType = genericInterfaceTypes
                    .Any(x => x.GetGenericTypeDefinition() == typeToMatch);
                    return !type.IsInterface && !type.IsAbstract && implementRequestType;
                });
            return requests.ToList();
        }

    }
}
