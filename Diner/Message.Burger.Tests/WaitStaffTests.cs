using System.Collections.Generic;
using Xunit;

namespace Message.Burger.Tests
{
    public class WaitStaffTests
    {
        [Fact]
        public void can_seat_customer() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus,new List<Table>{new Table(5)});
            waitStaff.Handle(new OrderMsgs.CustomerArrived(4));
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.CustomerSeated>(bus.Received[0]);
        }

        [Fact]
        public void can_ask_customers_to_wait() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus,new List<Table>{new Table(5)});
            waitStaff.Handle(new OrderMsgs.CustomerArrived(4));
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.CustomerSeated>(bus.Received[0]);

            bus.Received.Clear();
            waitStaff.Handle(new OrderMsgs.CustomerArrived(4));
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.CustomerAskedToWait>(bus.Received[0]);

        }
        [Fact]
        public void can_take_order() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus,new List<Table>());
            waitStaff.Handle(new OrderMsgs.FoodRequested());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.OrderIn>(bus.Received[0]);
            //todo: test correct table used
        }
        [Fact]
        public void can_deliver_order() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus, new List<Table>());
            waitStaff.Handle(new OrderMsgs.OrderUp());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.OrderDelivered>(bus.Received[0]);
            //todo: test correct table used
        }
    }
}
