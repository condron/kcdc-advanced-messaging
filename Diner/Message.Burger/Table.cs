using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger
{
    public class Table
    {
        public enum TState
        {
            Ready,
            Seated,
            Dirty
        }
        public int Size { get; }
        public Table.TState State { get; private set; }


        public bool TrySeat(int partySize) {
            if (State != TState.Ready) {
                return false;
            }
            if(partySize > Size) {
                return false;
            }

            State = TState.Seated;
            return true;
        }
        public Table(int size) {
            Size = size;
        }
    }
}
