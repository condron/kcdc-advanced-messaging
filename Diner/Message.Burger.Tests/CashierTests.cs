﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Message.Burger.Tests
{
    public class TestBus:
        IPublish
    {
        public List<Message> Received = new List<Message>();
        public void Publish(Message message) {
            Received.Add(message);
        }
    }
    public class CashierTests
    {
        //todo: add more test cases for message and state variations
        [Fact]
        public void can_total_order() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            cashier.Handle(new CompletedOrder());
            Assert.Single(bus.Received);
            Assert.IsType<OrderTotaled>(bus.Received[0]);
            //todo: check sum logic
        }
        [Fact]
        public void can_accept_payment() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            cashier.Handle(new PaymentTendered());
            Assert.Single(bus.Received);
            Assert.IsType<OrderPaid>(bus.Received[0]);
            //todo: check payment processing logic
        }
    }
}
