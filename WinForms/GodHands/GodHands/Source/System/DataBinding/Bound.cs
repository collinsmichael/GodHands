using System;
using System.Collections.Generic;
using System.Text;

namespace GodHands {
    public interface IBound {
        string GetUrl();
        string GetText();
        int GetPos();
        void SetPos(int pos);
        int GetLen();
    }
}
