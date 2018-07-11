using System;
using System.Collections.Generic;
using ReactiveDomain.Messaging.Bus;

namespace Message.Burger.Tests {
    public class TestBus:
        IBus
    {
        public List<ReactiveDomain.Messaging.Message> Received = new List<ReactiveDomain.Messaging.Message>();
        public void Publish(ReactiveDomain.Messaging.Message message) {
            Received.Add(message);
        }

        public IDisposable Subscribe<T>(IHandle<T> handler) where T : ReactiveDomain.Messaging.Message {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T>(IHandle<T> handler) where T : ReactiveDomain.Messaging.Message {
            throw new NotImplementedException();
        }

        public bool HasSubscriberFor<T>(bool includeDerived = false) where T : ReactiveDomain.Messaging.Message {
            throw new NotImplementedException();
        }

        public string Name { get; }
    }
}