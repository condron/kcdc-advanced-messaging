using System.Collections.Generic;

namespace Message.Burger.Tests {
    public class TestBus:
        IPublish
    {
        public List<Message> Received = new List<Message>();
        public void Publish(Message message) {
            Received.Add(message);
        }
    }
}