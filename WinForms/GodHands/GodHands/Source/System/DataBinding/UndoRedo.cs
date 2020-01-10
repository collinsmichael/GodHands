using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public static class UndoRedo {
        private static Stack<ICommand> undo = new Stack<ICommand>();
        private static Stack<ICommand> redo = new Stack<ICommand>();

        // ****************************************************************
        // Performs an action and add to undo stack
        // ****************************************************************
        public static bool Exec(ICommand cmd) {
            if (!cmd.Exec()) {
                return false;
            }
            undo.Push(cmd);
            redo.Clear();
            return true;
        }

        // ****************************************************************
        // Undo the last action
        // ****************************************************************
        public static bool Undo() {
            if (undo.Count > 0) {
                ICommand cmd = undo.Pop();
                redo.Push(cmd);
                cmd.Undo();
                return Logger.Pass("Undo");
            }
            return true;
        }

        // ****************************************************************
        // Redo the last undone action
        // ****************************************************************
        public static bool Redo() {
            if (redo.Count > 0) {
                ICommand cmd = redo.Pop();
                undo.Push(cmd);
                cmd.Redo();
                return Logger.Pass("Redo");
            }
            return true;
        }
    }
}
