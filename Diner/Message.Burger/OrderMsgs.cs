
using System.Collections.Generic;
using ReactiveDomain.Messaging;


namespace Message.Burger {
    public class OrderMsgs {
        //cashier
        public class OrderPaid : ReactiveDomain.Messaging.Message {
            public readonly int TicketNumber;

            public OrderPaid(
                int ticketNumber) {
                TicketNumber = ticketNumber;
            }
        }

        public class OrderTotaled : ReactiveDomain.Messaging.Message {
            public readonly int TicketId;
            public readonly int Total;

            public OrderTotaled(
                int ticketId,
                int total) {
                TicketId = ticketId;
                Total = total;
            }
        }
        //cash only
        public enum PaymentType {
            Cash
        }
        public class PaymentTendered : ReactiveDomain.Messaging.Message {
            public readonly int TicketNumber;
            public readonly PaymentType Type;
            public readonly int Amount;

            public PaymentTendered(
                int ticketNumber,
                PaymentType type,
                int amount) {
                Amount = amount;
                TicketNumber = ticketNumber;
                Type = type;
            }
        }
        public class ChangeReturned : ReactiveDomain.Messaging.Message {
            public readonly int Amount;
            public readonly int TicketNumber;
            public ChangeReturned(
                int ticketNumber,
                int amount) {
                Amount = amount;
                TicketNumber = ticketNumber;
            }
        }
        public class PaymentRejected : ReactiveDomain.Messaging.Message {
            public readonly string Reason;

            public PaymentRejected(string reason) {
                Reason = reason;
            }
        }

        public class CompletedOrder : ReactiveDomain.Messaging.Message {
            public readonly int TicketId;
            public readonly List<string> MenuItems;

            public CompletedOrder(
                int ticketId,
                List<string> menuItems) {
                TicketId = ticketId;
                MenuItems = menuItems;
            }
        }
        //waitstaff
        public class OrderDelivered : ReactiveDomain.Messaging.Message { }

        public class OrderIn : ReactiveDomain.Messaging.Message { }

        public class CustomerSeated : ReactiveDomain.Messaging.Message { }

        public class CustomerAskedToWait : ReactiveDomain.Messaging.Message { }

        public class OrderUp : ReactiveDomain.Messaging.Message { }

        public class FoodRequested : ReactiveDomain.Messaging.Message { }

        public class CustomerArrived : ReactiveDomain.Messaging.Message {
            public readonly int PartySize;
            public readonly int TicketNumber;

            public CustomerArrived(
                int partySize,
                int ticketNumber) {
                PartySize = partySize;
                TicketNumber = ticketNumber;
            }
        }

        public class TicketNotFound : ReactiveDomain.Messaging.Message {
            public readonly int TicketNumber;
            public TicketNotFound(int ticketNumber) {
                TicketNumber = ticketNumber;
            }
        }

        public class InsufficientFunds : ReactiveDomain.Messaging.Message {
            public readonly int TicketNumber;
            public readonly int AmountPresented;
            public readonly int AmountDue;

            public InsufficientFunds(
                int ticketNumber,
                int amountPresented,
                int amountDue) {
                TicketNumber = ticketNumber;
                AmountPresented = amountPresented;
                AmountDue = amountDue;
            }
        }
    }
}
