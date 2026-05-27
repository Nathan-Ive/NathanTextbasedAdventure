using RulerOfTheTomb.Combat;

namespace RulerOfTheTomb.Items
{
    /// <summary>
    /// An item that can be worn or wielded in an equipment slot.
    /// Modifies actor stats while equipped and contributes to Status if aligned.
    /// </summary>
    public class Equipment : Item
    {
        /// <summary>
        /// The slot this equipment must be placed in (LeftHand, RightHand, or Armor).
        /// </summary>
        public EquipmentSlot Slot { get; protected set; }

        /// <summary>
        /// Whether this equipment takes up both hand slots when equipped.
        /// Only meaningful for hand-slot items (For example the Starting Spear, the Blessed Sword, two handed. The Cursed Mace, one handed).
        /// </summary>
        public bool IsTwoHanded { get; protected set; }

        // --- Stat modifiers applied while this equipment is worn ---
        public int StrengthBonus { get; protected set; }
        public int MagicBonus { get; protected set; }
        public int DefenseBonus { get; protected set; }
        public int MagicDefenseBonus { get; protected set; }
        public int SpeedBonus { get; protected set; }

        /// <summary>
        /// Constructs a piece of equipment with the given slot, stat bonuses, and alignment.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="description">Flavor description.</param>
        /// <param name="slot">The slot this equipment occupies.</param>
        /// <param name="strBonus">Strength bonus while equipped.</param>
        /// <param name="magBonus">Magic bonus while equipped.</param>
        /// <param name="defBonus">Defense bonus while equipped.</param>
        /// <param name="mdefBonus">Magic Defense bonus while equipped.</param>
        /// <param name="spdBonus">Speed bonus while equipped.</param>
        /// <param name="isTwoHanded">True if this equipment occupies both hand slots.</param>
        /// <param name="alignment">The item's alignment. Defaults to Neutral.</param>
        public Equipment(string name, string description, EquipmentSlot slot,
                         int strBonus = 0, int magBonus = 0,
                         int defBonus = 0, int mdefBonus = 0, int spdBonus = 0,
                         bool isTwoHanded = false,
                         Alignment alignment = Alignment.Neutral)
            : base(name, description, alignment)
        {
            Slot = slot;
            IsTwoHanded = isTwoHanded;
            StrengthBonus = strBonus;
            MagicBonus = magBonus;
            DefenseBonus = defBonus;
            MagicDefenseBonus = mdefBonus;
            SpeedBonus = spdBonus;
        }
    }
}
