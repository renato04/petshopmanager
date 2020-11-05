using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Application
{
    public interface IAsyncRequestHandler<TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest data);
    }
}
