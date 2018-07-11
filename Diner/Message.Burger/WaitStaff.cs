using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger
{
    public class WaitStaff:
        IHandle<OrderMsgs.CustomerArrived>,
        IHandle<OrderMsgs.FoodRequested>,
        IHandle<OrderMsgs.OrderUp> {

        private readonly IPublish _bus;

        public WaitStaff(IPublish bus) {
            _bus = bus;
        }
        public void Handle(OrderMsgs.CustomerArrived msg) {
            //seat customer
            _bus.Publish(new OrderMsgs.CustomerSeated());
        }

        public void Handle(OrderMsgs.FoodRequested msg) {
            //take order
            _bus.Publish(new OrderMsgs.OrderIn());
        }

        public void Handle(OrderMsgs.OrderUp msg) {
            //deliver food
            _bus.Publish(new OrderMsgs.OrderDelivered());
        }
    }


}
