using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Message.Burger.Tests
{
    public class CashierTests
    {
        //todo: add more test cases for message and state variations
        [Fact]
        public void can_total_order() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            cashier.Handle(new OrderMsgs.CompletedOrder());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.OrderTotaled>(bus.Received[0]);
            //todo: check sum logic
        }
        [Fact]
        public void can_accept_payment() {
            var bus = new TestBus();
            var cashier = new Cashier(bus);
            cashier.Handle(new OrderMsgs.PaymentTendered());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.OrderPaid>(bus.Received[0]);
            //todo: check payment processing logic
        }
    }
}
