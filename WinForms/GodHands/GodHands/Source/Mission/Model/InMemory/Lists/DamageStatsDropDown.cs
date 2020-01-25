using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace GodHands {
    public class DamageStatsDropDown : StringConverter {
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
            List<string> list = Model.damage_stats.GetList();
            return new StandardValuesCollection(list);
        }
    }
}
