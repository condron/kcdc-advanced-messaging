using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger
{
    public class WaitStaff:
        IHandle<CustomerArrived>,
        IHandle<FoodRequested>,
        IHandle<OrderUp> {

        private readonly IPublish _bus;

        public WaitStaff(IPublish bus) {
            _bus = bus;
        }
        public void Handle(CustomerArrived msg) {
            //seat customer
            _bus.Publish(new CustomerSeated());
        }

        public void Handle(FoodRequested msg) {
            //take order
            _bus.Publish(new OrderIn());
        }

        public void Handle(OrderUp msg) {
            //deliver food
            _bus.Publish(new OrderDelivered());
        }
    }

    public class OrderDelivered : Message { }

    public class OrderIn : Message { }

    public class CustomerSeated : Message { }

    public class OrderUp : Message { }

    public class FoodRequested : Message { }

    public class CustomerArrived : Message { }
}
