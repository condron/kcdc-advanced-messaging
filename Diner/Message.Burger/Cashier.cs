namespace Message.Burger {
    public class Cashier:
        IHandle<OrderMsgs.CompletedOrder>,
        IHandle<OrderMsgs.PaymentTendered>{
        private readonly IPublish _bus;

        public Cashier(IPublish bus) {
            _bus = bus;
        }

        public void Handle(OrderMsgs.CompletedOrder msg) {
            //todo: total order
            _bus.Publish(new OrderMsgs.OrderTotaled());
        }

        public void Handle(OrderMsgs.PaymentTendered msg) {
            //todo: process payment
            _bus.Publish(new OrderMsgs.OrderPaid());
        }
    }

  
}
