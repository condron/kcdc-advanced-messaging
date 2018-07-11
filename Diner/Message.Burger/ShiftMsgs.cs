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
        public class EndOfShift:Message{}
        public class ShiftTake:Message
        {
            public readonly decimal Take;

            public ShiftTake(decimal take) {
                Take = take;
            }
        }
    }
}
