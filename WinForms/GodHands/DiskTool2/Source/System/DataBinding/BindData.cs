using System;
using System.Collections.Generic;
using System.Text;

namespace GodHands {
    // ********************************************************************
    // Updates a string (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindString : ICommand {
        private BaseClass obj;
        private string old;
        private string val;
        private int delta;
        private int len;

        public BindString(BaseClass bound, int delta, int len, string val) {
            this.obj = bound;
            this.delta = delta;
            this.len = len;
            this.val = val;
            this.old = RamDisk.GetString(obj.GetPos()+delta, len);
        }

        public bool Exec() {
            RamDisk.SetString(obj.GetPos() + delta, len, val);
            Publisher.Publish(obj);
            return Logger.Info("BindString.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetString(obj.GetPos() + delta, len, old);
            Publisher.Publish(obj);
            return Logger.Info("BindString.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetString(obj.GetPos() + delta, len, val);
            Publisher.Publish(obj);
            return Logger.Info("BindString.Redo("+val+")");
        }
    }

    // ********************************************************************
    // Updates an array (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindArray : ICommand {
        private BaseClass obj;
        private byte[] old;
        private byte[] val;
        private int pos;
        private int len;

        public BindArray(BaseClass bound, int pos, int len, byte[] val) {
            this.obj = bound;
            this.pos = pos;
            this.len = len;
            this.val = val;
            this.old = new byte[len];
            RamDisk.Get(pos, len, old);
        }

        public bool Exec() {
            RamDisk.Set(pos, len, val);
            Publisher.Publish(obj);
            return Logger.Info("BindArray.Exec("+len+")");
        }

        public bool Undo() {
            RamDisk.Set(pos, len, old);
            Publisher.Publish(obj);
            return Logger.Info("BindArray.Undo("+len+")");
        }

        public bool Redo() {
            RamDisk.Set(pos, len, val);
            Publisher.Publish(obj);
            return Logger.Info("BindArray.Redo("+len+")");
        }
    }

    // ********************************************************************
    // Updates a uint (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindU32 : ICommand {
        private BaseClass obj;
        private uint old;
        private uint val;
        private int delta;

        public BindU32(BaseClass bound, int delta, uint val) {
            this.obj = bound;
            this.delta = delta;
            this.val = val;
            this.old = RamDisk.GetU32(obj.GetPos()+delta);
        }

        public bool Exec() {
            RamDisk.SetU32(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindU32.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetU32(obj.GetPos() + delta, old);
            Publisher.Publish(obj);
            return Logger.Info("BindU32.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetU32(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindU32.Redo("+val+")");
        }
    }

    // ********************************************************************
    // Updates a uint (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindS32 : ICommand {
        private BaseClass obj;
        private int old;
        private int val;
        private int delta;

        public BindS32(BaseClass bound, int delta, int val) {
            this.obj = bound;
            this.delta = delta;
            this.val = val;
            this.old = RamDisk.GetS32(obj.GetPos()+delta);
        }

        public bool Exec() {
            RamDisk.SetS32(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindS32.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetS32(obj.GetPos() + delta, old);
            Publisher.Publish(obj);
            return Logger.Info("BindS32.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetS32(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindS32.Redo("+val+")");
        }
    }

    // ********************************************************************
    // Updates a ushort (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindU16 : ICommand {
        private BaseClass obj;
        private ushort old;
        private ushort val;
        private int delta;

        public BindU16(BaseClass bound, int delta, ushort val) {
            this.obj = bound;
            this.delta = delta;
            this.val = val;
            this.old = RamDisk.GetU16(obj.GetPos()+delta);
        }

        public bool Exec() {
            RamDisk.SetU16(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindU16.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetU16(obj.GetPos() + delta, old);
            Publisher.Publish(obj);
            return Logger.Info("BindU16.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetU16(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindU16.Redo("+val+")");
        }
    }

    // ********************************************************************
    // Updates a short (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindS16 : ICommand {
        private BaseClass obj;
        private short old;
        private short val;
        private int delta;

        public BindS16(BaseClass bound, int delta, short val) {
            this.obj = bound;
            this.delta = delta;
            this.val = val;
            this.old = RamDisk.GetS16(obj.GetPos()+delta);
        }

        public bool Exec() {
            RamDisk.SetS16(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindS16.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetS16(obj.GetPos() + delta, old);
            Publisher.Publish(obj);
            return Logger.Info("BindS16.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetS16(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindS16.Redo("+val+")");
        }
    }

    // ********************************************************************
    // Updates a byte (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindU8 : ICommand {
        private BaseClass obj;
        private byte old;
        private byte val;
        private int delta;

        public BindU8(BaseClass bound, int delta, byte val) {
            this.obj = bound;
            this.delta = delta;
            this.val = val;
            this.old = RamDisk.GetU8(obj.GetPos()+delta);
        }

        public bool Exec() {
            RamDisk.SetU8(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindU8.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetU8(obj.GetPos() + delta, old);
            Publisher.Publish(obj);
            return Logger.Info("BindU8.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetU8(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindU8.Redo("+val+")");
        }
    }

    // ********************************************************************
    // Updates a short (can be undone) and Publishes changes to Publisher
    // ********************************************************************
    public class BindS8 : ICommand {
        private BaseClass obj;
        private sbyte old;
        private sbyte val;
        private int delta;

        public BindS8(BaseClass bound, int delta, sbyte val) {
            this.obj = bound;
            this.delta = delta;
            this.val = val;
            this.old = RamDisk.GetS8(obj.GetPos()+delta);
        }

        public bool Exec() {
            RamDisk.SetS8(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindS8.Exec("+val+")");
        }

        public bool Undo() {
            RamDisk.SetS8(obj.GetPos() + delta, old);
            Publisher.Publish(obj);
            return Logger.Info("BindS8.Undo("+old+")");
        }

        public bool Redo() {
            RamDisk.SetS8(obj.GetPos() + delta, val);
            Publisher.Publish(obj);
            return Logger.Info("BindS8.Redo("+val+")");
        }
    }
}
