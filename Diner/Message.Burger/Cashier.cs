using System.Collections.Generic;

namespace Message.Burger {
    public class Cashier:
        IHandle<OrderMsgs.CompletedOrder>,
        IHandle<OrderMsgs.PaymentTendered>,
        IHandle<ShiftMsgs.EndOfShift>
    {
        private readonly IPublish _bus;
        private decimal _till;
        //todo: use value types here
        //ticketId, total
        private readonly Dictionary<int,int> _tickets = new Dictionary<int, int>();

        public Cashier(IPublish bus) {
            _bus = bus;
        }

        public void Handle(OrderMsgs.CompletedOrder ticket) {
            //todo: need menu and foot items
            //assuming it is a dollar menu for now
            var amount = ticket.MenuItems.Count;
            _tickets.Add(ticket.TicketId, amount);
            _bus.Publish(new OrderMsgs.OrderTotaled(ticket.TicketId,amount));
        }

        public void Handle(OrderMsgs.PaymentTendered payment) {
            
            if(payment.Type != OrderMsgs.PaymentType.Cash) {
                _bus.Publish(new OrderMsgs.PaymentRejected("We are Cash only."));
                return;
            }
            
            if(! _tickets.TryGetValue(payment.TicketNumber, out var amount)){
                _bus.Publish(new OrderMsgs.TicketNotFound(payment.TicketNumber));
                return;
            }
            if(payment.Amount < amount) {
                _bus.Publish(new OrderMsgs.InsufficientFunds(payment.TicketNumber,payment.Amount,amount));
                return;
            }
            if(payment.Amount > amount) {
                _bus.Publish(new OrderMsgs.ChangeReturned(payment.TicketNumber, payment.Amount - amount));
            }
            _till += amount;
            _bus.Publish(new OrderMsgs.OrderPaid(payment.TicketNumber));
        }

        public void Handle(ShiftMsgs.EndOfShift msg) {
            _bus.Publish(new ShiftMsgs.ShiftTake(_till));
        }
    }

  
}
