using System;
using System.Collections.Generic;

namespace Bobs.Burgers {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }
    public class Shift {
        private readonly ShiftManager _shiftManager;
        private readonly List<WaitStaff> _waitStaff;
        private readonly List<Cook> _cooks;
        public DiningRoom Room { get; private set; }

        public Shift(ShiftManager shiftManager, DiningRoom room) {
            _shiftManager = shiftManager;
            _waitStaff = new List<WaitStaff>();
            _cooks = new List<Cook>();
            Room = room;
        }
        public void ClockIn(WaitStaff waitStaff) {
            _waitStaff.Add(waitStaff);
        }
        public void ClockIn(Cook cook) {
            _cooks.Add(cook);
        }

        public void End() {
            //todo: implement end shift (dispose??)
        }
        public class Ticket {
            private static int _ticketId;
            public int TicketId = ++_ticketId;
            public int Total { get; private set; }
            public List<string> Items;

            public void TotalBill(Cashier cashier) {
                Total = cashier.PriceItems(Items);
            }
        }
        public class DiningRoom {
            public List<Table> Tables;
            public DiningRoom() {
                Tables = new List<Table>();
                for (int i = 0; i < 8; i++) {
                    Tables.Add(new Table(4));
                }
            }
        }
        public class Table {
            public readonly int Size;
            public bool Occupied { get; private set; }
            public bool TrySeat(Party party) {
                if (party.Size <= Size) {
                    Occupied = true;
                    party.Seat(this);
                    return true;
                }
                return false;
            }
            public Table(int size) {
                Size = size;
            }
        }

        public class ShiftManager {
            private Shift _shift;
            private Manager _manager;
            private List<Ticket> _spike;
            public void StartShift(DiningRoom room, Manager manager) {
                _shift = new Shift(this, room);
                _manager = manager;
                _spike = new List<Ticket>();
            }
            public void SpikeTicket(Ticket ticket) {
                _spike.Add(ticket);
            }
            public void EndShift() {
                _shift.End();
                _manager.ReviewTickets(_spike);

            }

        }
        public class Manager {
            private List<Ticket> _daysTickets = new List<Ticket>();
            public void ReviewTickets(List<Ticket> ticketSpike) {
                _daysTickets.AddRange(ticketSpike);
                //todo: audit tickets
                //check for missing ids, unpaid, and bad totals
            }

        }

        public class WaitStaff {
            private Cook _cook;
            private List<Tuple<Party, Table>> _parties = new List<Tuple<Party, Table>>();
            public WaitStaff(Cook cook) {
                _cook = cook;
            }
            public void SeatCustomer(Party customer, Table table) {
                _parties.Add(new Tuple<Party, Table>(customer, table));
            }
            public void TakeOrder(Party party) {

            }

            public void DeliverOrder(Ticket ticket) {
                //todo: find the right table/party for this order
                throw new NotImplementedException();
            }
        }

        public class Cook {
            public Cook() {

            }
            private Queue<Tuple<Ticket, WaitStaff>> _orderQueue = new Queue<Tuple<Ticket, WaitStaff>>();
            public void OrderIn(Ticket order, WaitStaff waitStaff) {
                _orderQueue.Enqueue(new Tuple<Ticket, WaitStaff>(order, waitStaff));
            }
            public void OrderUp(WaitStaff waitStaff) {
                //todo: make sure we have the right waitStaff for the next order
                if (_orderQueue.Count > 0) {
                    var ticket = _orderQueue.Dequeue();
                    waitStaff.DeliverOrder(ticket.Item1);
                }
            }

        }
        public class Cashier {
            public Dictionary<string, uint> Menu { get; private set; }

            public Cashier(List<Tuple<string, uint>> menuItems) {
                Menu = new Dictionary<string, uint>();
                foreach (var item in menuItems) {
                    Menu.Add(item.Item1, item.Item2);
                }
            }

            internal int PriceItems(List<string> items) {
                uint total = 0;
                foreach (var item in items) {
                    total += Menu[item];
                }
                return (int)total;
            }
            public bool PayBill(Ticket bill, int cash) {
                //todo check payment
                return true;
            }
        }
        public class Party {
            public readonly int Size;
            private readonly int cash;

            public Table Table { get; private set; }
            public bool HasPaid { get; private set; }
            private Ticket Bill { get; set; }
            public Party(int size, int cash) {
                Size = size;
                this.cash = cash;
            }
            public void Seat(Table table) {
                Table = table;
            }
            public void PayBill(Cashier manager) {
                HasPaid = manager.PayBill(Bill, cash);
            }
        }
    }
}
