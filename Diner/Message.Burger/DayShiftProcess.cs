using ReactiveDomain.Messaging.Bus;

namespace Message.Burger
{
    public class DayShiftProcess:
        IHandle<OrderMsgs.CustomerArrived> {
        private readonly IBus _mainBus;

        public DayShiftProcess(IBus mainBus) {
            _mainBus = mainBus;
        }

        public void Handle(OrderMsgs.CustomerArrived arrived) {
            _mainBus.Publish(new OrderMsgs.CustomerAssigned(arrived.PartySize,arrived.TicketNumber));
        }
    }
}
