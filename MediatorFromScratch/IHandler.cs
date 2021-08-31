﻿using System.Threading.Tasks;

namespace MediatorFromScratch
{
    public interface IHandler<in TRequest, TResponse> 
        where TRequest: IRequest<TResponse>
    {
        Task<TResponse> HandlerAsync(TRequest request);
    }
}
