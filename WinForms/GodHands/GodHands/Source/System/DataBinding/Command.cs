using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    // ********************************************************************
    // Performs an action which can later be undone
    // ********************************************************************
    public interface ICommand {
        bool Exec();
        bool Undo();
        bool Redo();
    }
}
