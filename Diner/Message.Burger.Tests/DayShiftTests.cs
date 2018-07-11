using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ReactiveDomain.Messaging.Bus;
using ReactiveUI;
using Xunit;

namespace Message.Burger.Tests
{
    public class DayShiftTests
    {
        [Fact]
        public void can_seat_customers() {
            var dinner = new Dinner();
            dinner.SetupDayShift();
            const int ticketId = 1;
            const int partySize = 4;
            var messages = new List<ReactiveDomain.Messaging.Message>();
            dinner.MainBus
                .Subscribe(new AdHocHandler<ReactiveDomain.Messaging.Message>(
                    msg => messages.Add(msg)));
            dinner.MainBus.Publish(new OrderMsgs.CustomerArrived(partySize,ticketId));
            Assert.Equal(3,messages.Count);
            //n.b. bus is currently sync calls through all registered handlers
            //this places the triggering event after the resulting events in the message list
            var arrived = messages[2] as OrderMsgs.CustomerArrived;
            Assert.NotNull(arrived);
            var assigned = messages[1] as OrderMsgs.CustomerAssigned;
            Assert.NotNull(assigned);
            var seated = messages[0] as OrderMsgs.CustomerSeated;
            Assert.NotNull(seated);
            Assert.Equal(partySize, seated.PartySize);
            Assert.Equal(ticketId, seated.TicketNumber);
        }
        [Fact]
        public void can_get_shift_take() {
            var dinner = new Dinner();
            dinner.SetupDayShift();
            for (int i = 0; i < 10; i++) {
                dinner.MainBus.Publish(new OrderMsgs.CompletedOrder(i,new List<string>{"fries"}));
                dinner.MainBus.Publish(new OrderMsgs.PaymentTendered(i,OrderMsgs.PaymentType.Cash,1));
            }

            decimal take = 0;
            dinner.MainBus.Subscribe(new AdHocHandler<ShiftMsgs.ShiftTake>(msg =>  take = msg.Take));
            dinner.MainBus.Publish(new ShiftMsgs.EndOfShift());
            Assert.Equal(10,take);
        }

    }
}
