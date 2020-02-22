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

    public static class UndoRedo {
        public static Stack<ICommand> undo = new Stack<ICommand>();
        public static Stack<ICommand> redo = new Stack<ICommand>();

        // ****************************************************************
        // Clear the Undo/Redo buffers
        // ****************************************************************
        public static bool Reset() {
            undo.Clear();
            redo.Clear();
            return true;
        }

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
                Logger.SetProgress(100);
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
                Logger.SetProgress(100);
                return Logger.Pass("Redo");
            }
            return true;
        }
    }
}
