using System;
using System.Collections.Generic;
using ReactiveDomain.Messaging.Bus;

namespace Message.Burger
{
    public class WaitStaff:
        IHandle<OrderMsgs.CustomerArrived>,
        IHandle<OrderMsgs.FoodRequested>,
        IHandle<OrderMsgs.OrderUp>,
        IHandle<OrderMsgs.OrderPaid> {

        private readonly IBus _bus;
        private readonly List<Table> _section;

        public WaitStaff(IBus bus,
                         List<Table> section) {
            _bus = bus;
            _section = section;
        }
        public void Handle(OrderMsgs.CustomerArrived party) {
            //seat customer
            foreach (var table in _section) {
                if(table.TrySeat(party.PartySize)) {
                    _bus.Publish(new OrderMsgs.CustomerSeated());
                    return;
                }
            }
            _bus.Publish(new OrderMsgs.CustomerAskedToWait());
        }

        public void Handle(OrderMsgs.FoodRequested msg) {
            //take order
            _bus.Publish(new OrderMsgs.OrderIn());
        }

        public void Handle(OrderMsgs.OrderUp msg) {
            //deliver food
            _bus.Publish(new OrderMsgs.OrderDelivered());
        }

        public void Handle(OrderMsgs.OrderPaid msg) {
            //customer has left, clear table
            throw new NotImplementedException();
        }
    }


}
