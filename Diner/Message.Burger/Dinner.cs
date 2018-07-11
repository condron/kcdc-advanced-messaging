using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveDomain.Messaging.Bus;

namespace Message.Burger
{
    public class Dinner
    {
        public IBus MainBus;
        public void SetupDayShift() {
        //manual wire-up
            MainBus = new InMemoryBus("main bus", false);
            var cashier = new Cashier(MainBus);
            MainBus.Subscribe<OrderMsgs.CompletedOrder>(cashier);
            MainBus.Subscribe<OrderMsgs.PaymentTendered>(cashier);
            MainBus.Subscribe<ShiftMsgs.EndOfShift>(cashier);
            var waiter = new WaitStaff(MainBus, new List<Table>{new Table(4), new Table(5)});
            MainBus.Subscribe<OrderMsgs.CustomerArrived>(waiter);
            MainBus.Subscribe<OrderMsgs.FoodRequested>(waiter);
            MainBus.Subscribe<OrderMsgs.OrderUp>(waiter);
            MainBus.Subscribe<OrderMsgs.OrderPaid>(waiter);
        }
    }
}
