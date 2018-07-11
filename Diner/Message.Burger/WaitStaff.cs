using System;
using System.Collections.Generic;
using ReactiveDomain.Messaging.Bus;

namespace Message.Burger {
    public class WaitStaff :
        IHandle<OrderMsgs.CustomerAssigned>,
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
        public void Handle(OrderMsgs.CustomerAssigned party) {
            //seat customer
            foreach (var table in _section) {
                if (table.TrySeat(party.PartySize)) {
                    _bus.Publish(new OrderMsgs.CustomerSeated(party.PartySize, party.TicketNumber));
                    return;
                }
            }
            _bus.Publish(new OrderMsgs.CustomerAskedToWait(party.PartySize, party.TicketNumber));
        }

        public void Handle(OrderMsgs.FoodRequested msg) {
            //take order
            _bus.Publish(new OrderMsgs.OrderIn());
        }

        public void Handle(OrderMsgs.OrderUp msg) {
            //deliver food
            //todo:add items to orderUp
            //todo: add ticketId to orderUp
            _bus.Publish(new OrderMsgs.OrderDelivered(1,new List<string>{"fries"}));
        }

        public void Handle(OrderMsgs.OrderPaid msg) {
            //todo: customer has left, clear table
            
        }
    }


}
