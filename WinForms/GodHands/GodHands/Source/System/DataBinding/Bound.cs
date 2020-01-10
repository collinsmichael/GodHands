using System;
using System.Collections.Generic;
using System.Text;

namespace GodHands {
    public interface IBound {
        string GetUrl();
        int GetPos();
        void SetPos(int pos);
    }
}
