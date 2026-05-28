using System.Collections.Generic;
using System.Linq;
using RulerOfTheTomb.Items;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// Holds all items belonging to an Actor: equipment slots (always present),
    /// key items (always present), and a bag (only usable if a Pouch (Key Item) is owned).
    /// </summary>
    public class Inventory
    {
        // --- Equipment slots (always exist, hold one Equipment item each) ---
        private readonly Dictionary<EquipmentSlot, Equipment> _equipment;

        // --- Key items (always exist, no capacity limit) ---
        private readonly List<KeyItem> _keyItems;

        // --- Bag (only functional when HasPouch is true) ---
        private readonly List<Item> _bag;

        /// <summary>
        /// Whether the owner of this inventory has a Pouch.
        /// When false, the Bag cannot be used and Add() will reject non-equipment, non-key items.
        /// </summary>
        public bool HasPouch { get; private set; }

        /// <summary>
        /// Constructs an empty inventory. Equipment and key-item slots exist by default,
        /// but the Bag remains unusable until a Pouch key-item is added.
        /// </summary>
        public Inventory()
        {
            _equipment = new Dictionary<EquipmentSlot, Equipment>
            {
                { EquipmentSlot.LeftHand, null },
                { EquipmentSlot.RightHand, null },
                { EquipmentSlot.Armor, null }
            };
            _keyItems = new List<KeyItem>();
            _bag = new List<Item>();
            HasPouch = false;
        }

        // ---------- Adding items ----------

        /// <summary>
        /// Attempts to add an item to the inventory.
        /// Key items always succeed. Other items require either an open bag (with pouch)
        /// or a free equipment slot to land in.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was successfully stored, false otherwise.</returns>
        public bool Add(Item item)
        {
            if (item is KeyItem key)
            {
                _keyItems.Add(key);
                if (key.IsPouch) HasPouch = true;
                return true;
            }

            if (HasPouch)
            {
                _bag.Add(item);
                return true;
            }

            // No pouch: the item has no home unless caller equips it manually.
            return false;
        }

        // ---------- Equipment handling ----------

        /// <summary>
        /// Equips a piece of equipment into the given slot. Handles two-handed weapons by
        /// clearing the opposite hand, and clears two-handed weapons from the opposite hand
        /// when equipping anything else.
        /// </summary>
        /// <param name="equipment">The equipment to wear.</param>
        /// <param name="slot">The slot to place it in.</param>
        /// <returns>A list of equipment that was displaced and needs to be handled by the caller (dropped, bagged, etc.).</returns>
        public List<Equipment> Equip(Equipment equipment, EquipmentSlot slot)
        {
            var displaced = new List<Equipment>();

            // If equipping a two-handed weapon to a hand slot, clear both hands first.
            if (equipment.IsTwoHanded && (slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand))
            {
                if (_equipment[EquipmentSlot.LeftHand] != null)
                    displaced.Add(_equipment[EquipmentSlot.LeftHand]);
                if (_equipment[EquipmentSlot.RightHand] != null)
                    displaced.Add(_equipment[EquipmentSlot.RightHand]);

                _equipment[EquipmentSlot.LeftHand] = null;
                _equipment[EquipmentSlot.RightHand] = null;
                _equipment[slot] = equipment;
                return displaced;
            }

            // If equipping a one-handed item to a hand, kick out any two-hander occupying the other hand.
            if (slot == EquipmentSlot.LeftHand || slot == EquipmentSlot.RightHand)
            {
                EquipmentSlot otherHand = slot == EquipmentSlot.LeftHand
                    ? EquipmentSlot.RightHand
                    : EquipmentSlot.LeftHand;

                if (_equipment[otherHand]?.IsTwoHanded == true)
                {
                    displaced.Add(_equipment[otherHand]);
                    _equipment[otherHand] = null;
                }
            }

            // Standard swap for the target slot.
            if (_equipment[slot] != null)
                displaced.Add(_equipment[slot]);

            _equipment[slot] = equipment;
            return displaced;
        }

        /// <summary>
        /// Removes and returns the equipment in the given slot, leaving it empty.
        /// </summary>
        /// <param name="slot">The slot to clear.</param>
        /// <returns>The previously equipped item, or null if the slot was empty.</returns>
        public Equipment Unequip(EquipmentSlot slot)
        {
            Equipment previous = _equipment[slot];
            _equipment[slot] = null;
            return previous;
        }

        /// <summary>
        /// Returns the equipment currently worn in the given slot, or null if empty.
        /// </summary>
        public Equipment GetEquipped(EquipmentSlot slot) => _equipment[slot];

        // ---------- Counting (for Player to ask about status & ending) ----------

        /// <summary>
        /// Counts equipped items aligned to Blessed.
        /// Used by Player to determine current Status.
        /// </summary>
        public int CountEquippedBlessed() =>
            _equipment.Values.Count(e => e != null && e.Alignment == Alignment.Blessed);

        /// <summary>
        /// Counts equipped items aligned to Cursed.
        /// Used by Player to determine current Status.
        /// </summary>
        public int CountEquippedCursed() =>
            _equipment.Values.Count(e => e != null && e.Alignment == Alignment.Cursed);

        /// <summary>
        /// Counts all owned items (equipped, bagged, and key items) aligned to Blessed.
        /// Used by Player to determine the ending.
        /// </summary>
        public int CountOwnedBlessed() => CountAllAligned(Alignment.Blessed);

        /// <summary>
        /// Counts all owned items (equipped, bagged, and key items) aligned to Cursed.
        /// Used by Player to determine the ending.
        /// </summary>
        public int CountOwnedCursed() => CountAllAligned(Alignment.Cursed);

        /// <summary>
        /// Returns the alignment of the tiebreaker key item from Scene 5, or null if absent.
        /// </summary>
        public Alignment? GetKeyItemAlignment()
        {
            KeyItem aligned = _keyItems.FirstOrDefault(k => k.Alignment != Alignment.Neutral);
            return aligned?.Alignment;
        }

        // ---------- Private helper ----------

        private int CountAllAligned(Alignment alignment)
        {
            int equipped = _equipment.Values.Count(e => e != null && e.Alignment == alignment);
            int bagged = _bag.OfType<Equipment>().Count(e => e.Alignment == alignment);
            int keyed = _keyItems.Count(k => k.Alignment == alignment);
            return equipped + bagged + keyed;
        }
    }
}
