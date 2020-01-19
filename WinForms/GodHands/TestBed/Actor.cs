using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace TestBed {
    public class Equip {
        public string ItemNamesList { get; set; }
        public string ItemList { get; set; }
        public string ItemCategory{ get; set; }
        public string Material { get; set; }

        public int STR { get; set; }
        public int AGL { get; set; }
        public int INT { get; set; }

        public int DP_Cur { get; set; }
        public int DP_Max { get; set; }
        public int PP_Cur { get; set; }
        public int PP_Max { get; set; }
        public int DamageType { get; set; }
        public int DamageStat { get; set; }
        public int DamageCost { get; set; }

        public int RangeX { get; set; }
        public int RangeY { get; set; }
        public int RangeZ { get; set; }
        public string RangeShape { get; set; }
        public int RangeAngle { get; set; }

        public int ClassHuman { get; set; }
        public int ClassPhantom { get; set; }
        public int ClassBeast { get; set; }
        public int ClassDragon { get; set; }
        public int ClassUndead { get; set; }
        public int ClassEvil { get; set; }
        public int AffinityEarth { get; set; }
        public int AffinityAir { get; set; }
        public int AffinityFire { get; set; }
        public int AffinityWater { get; set; }
        public int AffinityLight { get; set; }
        public int AffinityDark { get; set; }
        public int TypeBlunt { get; set; }
        public int TypeEdged { get; set; }
        public int TypePiercing { get; set; }
    }

    public class Gem {
        public string ItemNamesList { get; set; }
        public string ItemList { get; set; }
        public string ItemCategory{ get; set; }
        public string Material { get; set; }

        public int STR { get; set; }
        public int AGL { get; set; }
        public int INT { get; set; }

        public int DP_Cur { get; set; }
        public int DP_Max { get; set; }
        public int PP_Cur { get; set; }
        public int PP_Max { get; set; }
        public int DamageType { get; set; }
        public int DamageStat { get; set; }
        public int DamageCost { get; set; }

        public string GemSpecialEffects { get; set; }

        public int ClassHuman { get; set; }
        public int ClassPhantom { get; set; }
        public int ClassBeast { get; set; }
        public int ClassDragon { get; set; }
        public int ClassUndead { get; set; }
        public int ClassEvil { get; set; }
        public int AffinityEarth { get; set; }
        public int AffinityAir { get; set; }
        public int AffinityFire { get; set; }
        public int AffinityWater { get; set; }
        public int AffinityLight { get; set; }
        public int AffinityDark { get; set; }
        public int TypeBlunt { get; set; }
        public int TypeEdged { get; set; }
        public int TypePiercing { get; set; }
    }

    public class Blade {
        public string ItemNamesList { get; set; }
        public string ItemList { get; set; }
        public string WepFile { get; set; }
        public string ItemCategory{ get; set; }
        public string Material { get; set; }

        public int STR { get; set; }
        public int AGL { get; set; }
        public int INT { get; set; }

        public int DP_Cur { get; set; }
        public int DP_Max { get; set; }
        public int PP_Cur { get; set; }
        public int PP_Max { get; set; }
        public int DamageType { get; set; }
        public int DamageStat { get; set; }
        public int DamageCost { get; set; }

        public int RangeX { get; set; }
        public int RangeY { get; set; }
        public int RangeZ { get; set; }
        public string RangeShape { get; set; }
        public int RangeAngle { get; set; }

        public int ClassHuman { get; set; }
        public int ClassPhantom { get; set; }
        public int ClassBeast { get; set; }
        public int ClassDragon { get; set; }
        public int ClassUndead { get; set; }
        public int ClassEvil { get; set; }
        public int AffinityEarth { get; set; }
        public int AffinityAir { get; set; }
        public int AffinityFire { get; set; }
        public int AffinityWater { get; set; }
        public int AffinityLight { get; set; }
        public int AffinityDark { get; set; }
        public int TypeBlunt { get; set; }
        public int TypeEdged { get; set; }
        public int TypePiercing { get; set; }
    }

    public class Grip {
        public string ItemNamesList { get; set; }
        public string ItemList { get; set; }
        public string WepFile { get; set; }
        public string ItemCategory{ get; set; }
        public string Material { get; set; }

        public int STR { get; set; }
        public int AGL { get; set; }
        public int INT { get; set; }

        public int DP_Cur { get; set; }
        public int DP_Max { get; set; }
        public int PP_Cur { get; set; }
        public int PP_Max { get; set; }
        public int DamageType { get; set; }
        public int DamageStat { get; set; }
        public int DamageCost { get; set; }

        public int RangeX { get; set; }
        public int RangeY { get; set; }
        public int RangeZ { get; set; }
        public string RangeShape { get; set; }
        public int RangeAngle { get; set; }

        public int ClassHuman { get; set; }
        public int ClassPhantom { get; set; }
        public int ClassBeast { get; set; }
        public int ClassDragon { get; set; }
        public int ClassUndead { get; set; }
        public int ClassEvil { get; set; }
        public int AffinityEarth { get; set; }
        public int AffinityAir { get; set; }
        public int AffinityFire { get; set; }
        public int AffinityWater { get; set; }
        public int AffinityLight { get; set; }
        public int AffinityDark { get; set; }
        public int TypeBlunt { get; set; }
        public int TypeEdged { get; set; }
        public int TypePiercing { get; set; }
    }

    public class Weapon {
        private Blade blade;
        private Grip grip;
        private Gem GemSlot1;
        private Gem GemSlot2;
        private Gem GemSlot3;
        [Category("01 Stats")] public string Name { get; set; }
        [Category("01 Stats")] public int STR { get; set; }
        [Category("01 Stats")] public int AGL { get; set; }
        [Category("01 Stats")] public int INT { get; set; }
        [Category("01 Stats")] public int DP_Cur { get; set; }
        [Category("01 Stats")] public int DP_Max { get; set; }
        [Category("01 Stats")] public int PP_Cur { get; set; }
        [Category("01 Stats")] public int PP_Max { get; set; }
        [Category("02 Damage")] public int DamageType { get; set; }
        [Category("02 Damage")] public int DamageStat { get; set; }
        [Category("02 Damage")] public int DamageCost { get; set; }
        [Category("02 Damage")] public int RangeX { get; set; }
        [Category("02 Damage")] public int RangeY { get; set; }
        [Category("02 Damage")] public int RangeZ { get; set; }
        [Category("02 Damage")] public string RangeShape { get; set; }
        [Category("02 Damage")] public int RangeAngle { get; set; }
        [Category("03 Classes")] public int Human { get; set; }
        [Category("03 Classes")] public int Phantom { get; set; }
        [Category("03 Classes")] public int Beast { get; set; }
        [Category("03 Classes")] public int Dragon { get; set; }
        [Category("03 Classes")] public int Undead { get; set; }
        [Category("03 Classes")] public int Evil { get; set; }
        [Category("04 Affinities")] public int Earth { get; set; }
        [Category("04 Affinities")] public int Air { get; set; }
        [Category("04 Affinities")] public int Fire { get; set; }
        [Category("04 Affinities")] public int Water { get; set; }
        [Category("04 Affinities")] public int Light { get; set; }
        [Category("04 Affinities")] public int Dark { get; set; }
        [Category("05 Types")] public int Blunt { get; set; }
        [Category("05 Types")] public int Edged { get; set; }
        [Category("05 Types")] public int Piercing { get; set; }
    }

    public class Shield : Equip {
        public string ItemNamesList { get; set; }
        public string ItemList { get; set; }
        public string WepFile { get; set; }
        public string ItemCategory{ get; set; }
        public string Material { get; set; }

        public int STR { get; set; }
        public int AGL { get; set; }
        public int INT { get; set; }

        public int DP_Cur { get; set; }
        public int DP_Max { get; set; }
        public int PP_Cur { get; set; }
        public int PP_Max { get; set; }
        public int DamageType { get; set; }
        public int DamageStat { get; set; }
        public int DamageCost { get; set; }

        private Gem GemSlot1;
        private Gem GemSlot2;
        private Gem GemSlot3;
        public string GemSpecialEffects { get; set; }

        public int RangeX { get; set; }
        public int RangeY { get; set; }
        public int RangeZ { get; set; }
        public string RangeShape { get; set; }
        public int RangeAngle { get; set; }

        public int ClassHuman { get; set; }
        public int ClassPhantom { get; set; }
        public int ClassBeast { get; set; }
        public int ClassDragon { get; set; }
        public int ClassUndead { get; set; }
        public int ClassEvil { get; set; }
        public int AffinityEarth { get; set; }
        public int AffinityAir { get; set; }
        public int AffinityFire { get; set; }
        public int AffinityWater { get; set; }
        public int AffinityLight { get; set; }
        public int AffinityDark { get; set; }
        public int TypeBlunt { get; set; }
        public int TypeEdged { get; set; }
        public int TypePiercing { get; set; }
    }


    public class Actor {
        public string ZndFile { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int STR { get; set; }
        public int AGL { get; set; }
        public int INT { get; set; }
    }
}
