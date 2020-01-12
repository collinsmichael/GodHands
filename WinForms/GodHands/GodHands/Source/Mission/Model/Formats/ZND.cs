﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ZND : IBound {
        private string url;
        private int pos;

        public ZND(string url, int pos) {
            this.url = url;
            this.pos = pos;
        }

        public string GetUrl() {
            return url;
        }

        public int GetPos() {
            return pos;
        }

        public void SetPos(int pos) {
            this.pos = pos;
        }
    }
}