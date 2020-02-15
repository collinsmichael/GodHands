using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class CompassDirectionDropDown  : StringConverter {
        public override bool
        GetStandardValuesSupported(ITypeDescriptorContext context) {
            return true;
        }

        public override bool
        GetStandardValuesExclusive(ITypeDescriptorContext context) {
            return true;
        }

        public override StandardValuesCollection
        GetStandardValues(ITypeDescriptorContext context) {
            List<string> list = new List<string>{
                "South", "West", "North", "East"
            };
            return new StandardValuesCollection(list);
        }
    }
}
