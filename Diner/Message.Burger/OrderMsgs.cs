using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger
{
    public class OrderMsgs
    {
        //cashier
        public class OrderPaid : Message
        {
            public readonly int TicketNumber;

            public OrderPaid(
                int ticketNumber) {
                TicketNumber = ticketNumber;
            }
        }

        public class OrderTotaled : Message { }

        public class PaymentTendered:Message
        {
            public readonly int TicketNumber;

            public PaymentTendered(
                int ticketNumber) {
                TicketNumber = ticketNumber;
            }
        }

        public class CompletedOrder:Message { }
        //waitstaff
        public class OrderDelivered : Message { }

        public class OrderIn : Message { }

        public class CustomerSeated : Message { }

        public class CustomerAskedToWait:Message{}

        public class OrderUp : Message { }

        public class FoodRequested : Message { }

        public class CustomerArrived : Message
        {
            public readonly int PartySize;
            public readonly int TicketNumber;

            public CustomerArrived(
                int partySize,
                int ticketNumber) {
                PartySize = partySize;
                TicketNumber = ticketNumber;
            }
        }
    }
}
