using System.Collections.Generic;
using RulerOfTheTomb.Skills;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// Base class for any participant in combat (Player or Enemy).
    /// Holds shared stats, an inventory, learned skills, and the core combat methods that all actors use.
    /// </summary>
    public abstract class Actor
    {
        // --- Identity ---
        public string Name { get; protected set; }

        // --- Core stats ---
        public int MaxHp { get; protected set; }
        public int CurrentHp { get; protected set; }
        public int MaxMp { get; protected set; }
        public int CurrentMp { get; protected set; }

        public int Strength { get; protected set; }
        public int Magic { get; protected set; }
        public int Defense { get; protected set; }
        public int MagicDefense { get; protected set; }
        public int Speed { get; protected set; }

        /// <summary>
        /// The actor's inventory, holding equipment, bag items, and key items.
        /// Always present, even for enemies that only use a few item slots.
        /// </summary>
        public Inventory Inventory { get; }

        /// <summary>
        /// All skills this actor has learned, each tracking its own remaining PP.
        /// Every actor knows at least the basic attack (unlimited PP).
        /// </summary>
        public List<LearnedSkill> Skills { get; }

        /// <summary>
        /// Whether the actor is still alive and able to act in combat.
        /// </summary>
        public bool IsAlive => CurrentHp > 0;

        /// <summary>
        /// Constructs an actor with the given base stats and a fresh empty inventory.
        /// CurrentHp and CurrentMp start at their maximum values. The basic attack is
        /// learned by default.
        /// </summary>
        /// <param name="name">Display name.</param>
        /// <param name="maxHp">Maximum HP.</param>
        /// <param name="maxMp">Maximum MP.</param>
        /// <param name="strength">Physical attack stat.</param>
        /// <param name="magic">Magic attack stat.</param>
        /// <param name="defense">Physical damage mitigation.</param>
        /// <param name="magicDefense">Magic damage mitigation.</param>
        /// <param name="speed">Turn order priority.</param>
        protected Actor(string name, int maxHp, int maxMp,
                       int strength, int magic, int defense, int magicDefense, int speed)
        {
            Name = name;
            MaxHp = maxHp;
            CurrentHp = maxHp;
            MaxMp = maxMp;
            CurrentMp = maxMp;
            Strength = strength;
            Magic = magic;
            Defense = defense;
            MagicDefense = magicDefense;
            Speed = speed;
            Inventory = new Inventory();
            Skills = new List<LearnedSkill>
            {
                new LearnedSkill(SkillLibrary.BasicAttack)
            };
        }

        /// <summary>
        /// Applies incoming damage to the actor after mitigation by Defense.
        /// </summary>
        /// <param name="rawDamage">The pre-mitigation damage value.</param>
        /// <returns>The actual damage dealt after defense is applied.</returns>
        public virtual int TakeDamage(int rawDamage)
        {
            int finalDamage = System.Math.Max(1, rawDamage - Defense);
            CurrentHp = System.Math.Max(0, CurrentHp - finalDamage);
            return finalDamage;
        }

        /// <summary>
        /// Applies damage directly, bypassing Defense mitigation.
        /// Used for status-effect damage like Hexed drain and Cursed recoil.
        /// </summary>
        /// <param name="amount">The exact HP to remove.</param>
        public void TakeRawDamage(int amount)
        {
            CurrentHp = System.Math.Max(0, CurrentHp - amount);
        }

        /// <summary>
        /// Restores HP up to the actor's maximum.
        /// </summary>
        /// <param name="amount">The amount of HP to restore.</param>
        public virtual void Heal(int amount)
        {
            CurrentHp = System.Math.Min(MaxHp, CurrentHp + amount);
        }

        /// <summary>
        /// Spends MP if the actor has enough. Returns whether the spend succeeded.
        /// </summary>
        /// <param name="amount">The MP cost to attempt.</param>
        /// <returns>True if MP was available and spent, false if the actor couldn't afford it.</returns>
        public virtual bool SpendMp(int amount)
        {
            if (CurrentMp < amount) return false;
            CurrentMp -= amount;
            return true;
        }

        /// <summary>
        /// Uses a learned skill against a target. Handles PP spending, damage/heal math,
        /// and firing the skill's OnUse event hook if present.
        /// </summary>
        /// <param name="learned">The learned skill to use.</param>
        /// <param name="target">The actor being targeted.</param>
        /// <returns>True if the skill was successfully used, false if out of PP.</returns>
        public virtual bool UseSkill(LearnedSkill learned, Actor target)
        {
            if (!learned.TrySpend()) return false;

            Skill skill = learned.Skill;

            switch (skill.EffectType)
            {
                case SkillEffectType.PhysicalDamage:
                    target.TakeDamage(Strength + skill.Power);
                    break;
                case SkillEffectType.MagicDamage:
                    target.TakeDamage(Magic + skill.Power);
                    break;
                case SkillEffectType.Heal:
                    target.Heal(skill.Power);
                    break;
                case SkillEffectType.Event:
                    // Event skills do nothing on their own; the OnUse callback handles it.
                    break;
            }

            skill.OnUse?.Invoke(this, target);
            return true;
        }
    }
}
