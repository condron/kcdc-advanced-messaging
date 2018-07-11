using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger {
    public interface IPublish
    {
        void Publish(Message message);
    }
}
