using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class Zone : BaseClass {
        public List<Room> rooms = new List<Room>();
        public List<Actor> actors = new List<Actor>();

        public Zone(string url, int pos) : base(url, pos) {
        }
    }
}
