using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class ItemNameGripDropDown : StringConverter {
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
            List<string> list = Model.grip_names.GetList();
            return new StandardValuesCollection(list);
        }
    }
}
