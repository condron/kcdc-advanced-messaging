using System;
using System.Collections.Generic;
using Xunit;

namespace Message.Burger.Tests {
    public class CashierTests {
        //todo: add more test cases for message and state variations
        [Fact]
        public void can_total_order() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            const int ticketId = 1;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                                                    ticketId, 
                                                    new List<string> { "fries", "fries", "cheese burger" }));
            Assert.Single(bus.Received);
            var totaled = bus.Received[0] as OrderMsgs.OrderTotaled;
            Assert.NotNull(totaled);

            Assert.Equal(ticketId, totaled.TicketId);
            Assert.Equal(3, totaled.Total);
        }
        [Fact]
        public void can_accept_exact_payment() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            const int ticketId = 1;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                                                    ticketId, 
                                                    new List<string> { "fries", "fries", "cheese burger" }));
           bus.Received.Clear();
            cashier.Handle(new OrderMsgs.PaymentTendered(
                                                    ticketId, 
                                                    OrderMsgs.PaymentType.Cash, 3));
            var paid = bus.Received[0] as OrderMsgs.OrderPaid;
            Assert.NotNull(paid);
            Assert.Single(bus.Received);
            Assert.Equal(ticketId, paid.TicketNumber);
        }
        [Fact]
        public void can_only_accept_cash_payment() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            const int ticketId = 1;
            var credit = (OrderMsgs.PaymentType) 1;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                ticketId, 
                new List<string> { "fries", "fries", "cheese burger" }));
            bus.Received.Clear();
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                credit, 
                3));
            var rejected = bus.Received[0] as OrderMsgs.PaymentRejected;
            Assert.NotNull(rejected);
            Assert.Single(bus.Received);
        }
        [Fact]
        public void can_return_change_on_payment() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            const int ticketId = 1;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                ticketId, 
                new List<string> { "fries", "fries", "cheese burger" }));
            bus.Received.Clear();
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                OrderMsgs.PaymentType.Cash, 4));
            Assert.Equal(2,bus.Received.Count);

            
            var change = bus.Received[0] as OrderMsgs.ChangeReturned;
            Assert.NotNull(change);
            Assert.Equal(ticketId, change.TicketNumber);
            Assert.Equal(1,change.Amount);

            var paid = bus.Received[1] as OrderMsgs.OrderPaid;
            Assert.NotNull(paid);
            Assert.Equal(ticketId, paid.TicketNumber);

        }
        [Fact]
        public void will_reject_insufficient_funds() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            const int ticketId = 1;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                ticketId, 
                new List<string> { "fries", "fries", "cheese burger" }));
            bus.Received.Clear();
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                OrderMsgs.PaymentType.Cash, 
                2));
            var rejected = bus.Received[0] as OrderMsgs.InsufficientFunds;
            Assert.NotNull(rejected);
            Assert.Single(bus.Received);
            Assert.Equal(ticketId, rejected.TicketNumber);
            Assert.Equal(2, rejected.AmountPresented);
            Assert.Equal(3, rejected.AmountDue);
        }
        [Fact]
        public void till_reject_untotaled_tickets() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            const int ticketId = 1;
            
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                OrderMsgs.PaymentType.Cash, 
                2));
            var rejected = bus.Received[0] as OrderMsgs.TicketNotFound;
            Assert.NotNull(rejected);
            Assert.Single(bus.Received);
            Assert.Equal(ticketId, rejected.TicketNumber);
        }
        [Fact]
        public void till_is_correct() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
             int ticketId = 1;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                ticketId, 
                new List<string> { "fries", "fries", "cheese burger" }));
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                OrderMsgs.PaymentType.Cash, 4));
            ticketId++;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                ticketId, 
                new List<string> { "fries", "fries", "cheese burger" }));
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                OrderMsgs.PaymentType.Cash, 3));
            ticketId++;
            cashier.Handle(new OrderMsgs.CompletedOrder(
                ticketId, 
                new List<string> { "fries", "fries", "cheese burger" }));
            cashier.Handle(new OrderMsgs.PaymentTendered(
                ticketId, 
                OrderMsgs.PaymentType.Cash, 5));
            bus.Received.Clear();
            cashier.Handle(new ShiftMsgs.EndOfShift());
            Assert.Single(bus.Received);
            var take = bus.Received[0] as ShiftMsgs.ShiftTake;
            Assert.NotNull(take);
            Assert.Equal(9,take.Take);
        }
    }
}
