using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger
{
    public class ShiftMsgs
    {
        public class EndOfShift:ReactiveDomain.Messaging.Message{}
        public class ShiftTake:ReactiveDomain.Messaging.Message
        {
            public readonly decimal Take;

            public ShiftTake(decimal take) {
                Take = take;
            }
        }
    }
}
