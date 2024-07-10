using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Abstractions.Messages
{
    [ExcludeFromTopology]   
    
    public interface INotificationEvent : IMessage
    {
    }
}
