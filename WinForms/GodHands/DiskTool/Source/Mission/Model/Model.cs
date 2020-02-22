using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public static class Model {
        // ********************************************************************
        // initialize model from file
        // ********************************************************************
        public static bool Open() {
            Model.Close();
            return true;
        }

        // ********************************************************************
        // release all resources
        // ********************************************************************
        public static bool Close() {
            return true;
        }
    }
}
