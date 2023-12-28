using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcomApp.Domain.Services
{
    public interface IMessagingService
    {
        Task SendMessageAsync<T>(T messageBody, string queueOrTopicName);
    }
}
