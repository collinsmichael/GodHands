using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Actor {
        private string _name;
        public string Name {
            get { return _name; }
            set { _name = value; }
        }

        private int _str;
        public int STR {
            get { return _str; }
            set { _str = value; }
        }

        private int _agl;
        public int AGL {
            get { return _agl; }
            set { _agl = value; }
        }

        private int _int;
        public int INT {
            get { return _int; }
            set { _int = value; }
        }

        public Actor(string _name, int _str, int _agl, int _int) {
            Name = _name;
            STR = _str;
            AGL = _agl;
            INT = _int;
        }
    }
}
