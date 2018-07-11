using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Message.Burger.Tests
{
    public class WaitStaffTests
    {
        [Fact]
        public void can_seat_customer() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus);
            waitStaff.Handle(new OrderMsgs.CustomerArrived());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.CustomerSeated>(bus.Received[0]);
            //todo: test correct table used
        }
        [Fact]
        public void can_take_order() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus);
            waitStaff.Handle(new OrderMsgs.FoodRequested());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.OrderIn>(bus.Received[0]);
            //todo: test correct table used
        }
        [Fact]
        public void can_deliver_order() {
            var bus = new TestBus();
            var waitStaff = new WaitStaff(bus);
            waitStaff.Handle(new OrderMsgs.OrderUp());
            Assert.Single(bus.Received);
            Assert.IsType<OrderMsgs.OrderDelivered>(bus.Received[0]);
            //todo: test correct table used
        }
    }
}
