using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Message.Burger {
    public interface IHandle<T> where T:Message
    {
        void Handle(T msg);
    }
    public abstract class Message{}
}
