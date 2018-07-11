using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger
{
    public class OrderMsgs
    {
        public class OrderPaid : Message { }

        public class OrderTotaled : Message { }

        public class PaymentTendered:Message { }

        public class CompletedOrder:Message { }
    }
}
