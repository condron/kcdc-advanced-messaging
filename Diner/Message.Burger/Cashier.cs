namespace Message.Burger {
    public class Cashier:
        IHandle<CompletedOrder>,
        IHandle<PaymentTendered>{
        private readonly IPublish _bus;

        public Cashier(IPublish bus) {
            _bus = bus;
        }

        public void Handle(CompletedOrder msg) {
            //todo: total order
            _bus.Publish(new OrderTotaled());
        }

        public void Handle(PaymentTendered msg) {
            //todo: process payment
            _bus.Publish(new OrderPaid());
        }
    }

    public class OrderPaid : Message { }

    public class OrderTotaled : Message { }

    public class PaymentTendered:Message { }

    public class CompletedOrder:Message { }
}
