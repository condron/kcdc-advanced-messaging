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
        public class OrderPaid : Message { }

        public class OrderTotaled : Message { }

        public class PaymentTendered:Message { }

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

            public CustomerArrived(
                int partySize) {
                PartySize = partySize;
            }
        }
    }
}
