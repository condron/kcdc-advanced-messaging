using System;

namespace Bobs.Burgers.Tests
{
    
    public class TicketTests
    {
        public static int Main() {
            var tt = new TicketTests();
            try{
            
                tt.tickets_increment();
                Console.WriteLine("Tests passed");

            }
            catch(Exception ex) {
                Console.WriteLine($"Tests Failed {ex.Message}");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
            return 0;
        }
       public void tickets_increment() {
           var t1= new Shift.Ticket();
           if(t1.TicketId != 1) throw new Exception("tickets don't increment");
           var t2= new Shift.Ticket();
           if(t2.TicketId != 2) throw new Exception("tickets don't increment");

       }
    }
}
