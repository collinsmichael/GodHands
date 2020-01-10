using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GodHands {
    // ********************************************************************
    // Recieves notification when the bound object is modified
    // ********************************************************************
    public interface ISubscriber {
        bool Notify(object obj);
    }
}
