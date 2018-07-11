using ReactiveDomain.Messaging.Bus;

namespace Message.Burger
{
    public class DayShiftProcess:
        IHandle<OrderMsgs.CustomerArrived>,
        IHandle<OrderMsgs.OrderDelivered> {
        private readonly IBus _mainBus;

        public DayShiftProcess(IBus mainBus) {
            _mainBus = mainBus;
        }

        public void Handle(OrderMsgs.CustomerArrived arrived) {
            _mainBus.Publish(new OrderMsgs.CustomerAssigned(arrived.PartySize,arrived.TicketNumber));
        }

        public void Handle(OrderMsgs.OrderDelivered delivered) {
           _mainBus.Publish(new OrderMsgs.CompletedOrder(delivered.TicketId, delivered.MenuItems));
        }
    }
}
