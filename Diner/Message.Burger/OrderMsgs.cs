using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger {
    public class OrderMsgs {
        //cashier
        public class OrderPaid : Message {
            public readonly int TicketNumber;

            public OrderPaid(
                int ticketNumber) {
                TicketNumber = ticketNumber;
            }
        }

        public class OrderTotaled : Message
        {
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
        public class PaymentTendered : Message {
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
        public class ChangeReturned : Message {
            public readonly int Amount;
            public readonly int TicketNumber;
            public ChangeReturned(
                int ticketNumber,
                int amount) {
                Amount = amount;
                TicketNumber = ticketNumber;
            }
        }
        public class PaymentRejected : Message {
            public readonly string Reason;

            public PaymentRejected(string reason) {
                Reason = reason;
            }
        }

        public class CompletedOrder : Message {
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
        public class OrderDelivered : Message { }

        public class OrderIn : Message { }

        public class CustomerSeated : Message { }

        public class CustomerAskedToWait : Message { }

        public class OrderUp : Message { }

        public class FoodRequested : Message { }

        public class CustomerArrived : Message {
            public readonly int PartySize;
            public readonly int TicketNumber;

            public CustomerArrived(
                int partySize,
                int ticketNumber) {
                PartySize = partySize;
                TicketNumber = ticketNumber;
            }
        }

        public class TicketNotFound : Message {
            public readonly int TicketNumber;
            public TicketNotFound(int ticketNumber) {
                TicketNumber = ticketNumber;
            }
        }

        public class InsufficientFunds : Message
        {
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
