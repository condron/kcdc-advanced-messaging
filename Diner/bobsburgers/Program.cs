using System;
using System.Collections.Generic;

namespace Bobs.Burgers {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
        }
    }
    public class Shift {
        private readonly Manager _shiftManager;
        private readonly List<WaitStaff> _waitStaff;
        private readonly List<Cook> _cooks;
        public DiningRoom Room { get; private set; }

        public Shift(Manager shiftManager, DiningRoom room) {
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
    public class Manager {
        private Shift _shift;
        public void StartShift(DiningRoom room) {
            _shift = new Shift(this, room);
        }
    }
    public class WaitStaff
    {
        public void SeatCustomer(){}
        public void TakeOrder(){}

        public void DeliverOrder(Ticket ticket) {
            //todo: find the right table/party for this order
            throw new NotImplementedException();
        }
    }

    public class Cook
    {
        public Cook() {
            
        }        
        private Queue<Tuple<Ticket,WaitStaff>> _orderQueue = new Queue<Tuple<Ticket, WaitStaff>>();
        public void OrderIn(Ticket order, WaitStaff waitStaff) {
            _orderQueue.Enqueue(new Tuple<Ticket, WaitStaff>(order,waitStaff));
        }
        public void OrderUp(WaitStaff waitStaff) {
            //todo: make sure we have the right waitStaff for the next order
            if(_orderQueue.Count > 0) {
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
