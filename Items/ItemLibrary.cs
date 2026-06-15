using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Items
{
    /// <summary>
    /// Static catalog of factory methods for Equipment and Key Items.
    /// Each method returns a fresh instance with that item's fixed stats and alignment.
    /// Usables are not handled here — they live as their own subclasses due to unique behavior.
    /// 
    /// Partially made with the help of AI. (Similar to the EnemyLibrary script. I did this one on my own,
    /// since I had the EnemyLibrary to use as an example for how I should set up instanced objects.
    /// But it did still use the improvements made by the AI itself, so I consider it partially made with the help of it.)
    /// </summary>
    public static class ItemLibrary
    {
        // ---------- Neutral Equipment ---------- //

        /// <summary>
        /// The starting two-handed weapon found in the other graves of Scene 1.
        /// </summary>
        public static Equipment CreateSpear() => new Equipment(
            name: "Worn Spear",
            description: "A flimsy spear from another man's grave. Long reach, but barely holding together.",
            slot: EquipmentSlot.RightHand,
            strBonus: 6,
            isTwoHanded: true,
            alignment: Alignment.Neutral
        );

        /// <summary>
        /// The starting armor found in the other graves of Scene 1.
        /// </summary>
        public static Equipment CreateLeatherArmor() => new Equipment(
            name: "Flaky Leather Armor",
            description: "Patchy leather that's seen better centuries. Better than nothing.",
            slot: EquipmentSlot.Armor,
            defBonus: 4,
            alignment: Alignment.Neutral);

        /// <summary>
        /// Late-game neutral armor looted from corpses in Scene 5.
        /// </summary>
        public static Equipment CreatePlatedArmor() => new Equipment(
            name: "Plated Armor",
            description: "Dented steel plates scavenged from a corpse. Heavier than leather, tougher too.",
            slot: EquipmentSlot.Armor,
            defBonus: 8,
            alignment: Alignment.Neutral);

        // ---------- Blessed Equipment ---------- //

        /// <summary>
        /// Blessed armor dropped by the Legless Fellow when the trap is deactivated.
        /// </summary>
        public static Equipment CreateSolarEclipseArmor() => new Equipment(
            name: "Solar-Eclipse Armor",
            description: "Pristine armor radiating with a guiding presence. A weight comes with wearing it.",
            slot: EquipmentSlot.Armor,
            defBonus: 14,
            mdefBonus: 6,
            spdBonus: -2,
            alignment: Alignment.Blessed);

        /// <summary>
        /// Blessed two-handed greatsword granted by the Riddling Trove on riddle success.
        /// </summary>
        public static Equipment CreateStellarMassGreatsword() => new Equipment(
            name: "Stellar-Mass Greatsword",
            description: "A massive sword that should be impossibly heavy. In your hands, it weighs nothing.",
            slot: EquipmentSlot.RightHand,
            strBonus: 13,
            magBonus: 8,
            defBonus: 3,
            mdefBonus: 3,
            isTwoHanded: true,
            alignment: Alignment.Blessed);

        // ---------- Cursed Equipment ---------- //

        /// <summary>
        /// Cursed one-handed mace dropped by the Legless Fellow when the trap crushes him.
        /// </summary>
        public static Equipment CreateMarrowSplitterMace() => new Equipment(
            name: "Marrow-Splitter Mace",
            description: "A heavy mace that jolts your bones with cruel energy the moment you grip it.",
            slot: EquipmentSlot.RightHand,
            strBonus: 14,
            spdBonus: 1,
            alignment: Alignment.Cursed);

        /// <summary>
        /// Cursed shield dropped by the Riddling Trove on riddle failure.
        /// </summary>
        public static Equipment CreateTormentorShield() => new Equipment(
            name: "Tormentor Shield",
            description: "A shield that hums with malice. It protects, but it also wants you to suffer.",
            slot: EquipmentSlot.LeftHand,
            strBonus: 8,
            defBonus: 10,
            mdefBonus: 12,
            spdBonus: 1,
            alignment: Alignment.Cursed);

        // ---------- Key Items ---------- //

        /// <summary>
        /// The Pouch from your own grave. Unlocks the Inventory's bag when added.
        /// </summary>
        public static KeyItem CreatePouch() => new KeyItem(
            name: "Pouch",
            description: "A small pouch tied at the hip. Big enough to carry whatever you find.",
            alignment: Alignment.Neutral,
            isPouch: true);

        /// <summary>
        /// Blessed key item dropped when the Radiant Warrior is killed first in Scene 5.
        /// Tiebreaker for the ending if owned blessed and cursed items are even.
        /// </summary>
        public static KeyItem CreateBlessedCube() => new KeyItem(
            name: "Radiant Cube",
            description: "A small cube of light pulled from the Radiant Warrior's remains. It has an overwhelming authority to it.",
            alignment: Alignment.Blessed);

        /// <summary>
        /// Cursed key item dropped when the Dull Scholar is killed first in Scene 5.
        /// Tiebreaker for the ending if owned blessed and cursed items are even.
        /// </summary>
        public static KeyItem CreateCursedCube() => new KeyItem(
            name: "Dull Cube",
            description: "A small cube of dark stone pulled from the Dull Scholar's remains. You feel powerful.",
            alignment: Alignment.Cursed);
    }
}