using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.MessageQueue.Daemon
{
    public static class MassTransitExtensions
    {
        public static void AddConsumers(this IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<MyMessageConsumer>();
        }
    }
}
