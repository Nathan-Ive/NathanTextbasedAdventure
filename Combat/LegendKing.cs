using System;
using RulerOfTheTomb.Scenes;

namespace RulerOfTheTomb.Combat
{
    /// <summary>
    /// The final boss. 
    /// A big health bar with stats a touch above a standard player but well below a fully Cursed one. 
    /// 
    /// Signature mechanic: once his health drops past threshold he begins charging his strongest attack, the fireball. 
    /// The charge takes several turns during which he lowers his guard and does not attack. 
    /// The window the player is meant to exploit 
    /// (Cursed players out-damage it, Blessed players outlast it via their revive).
    /// </summary>
    public class LegendKing : FinalBoss
    {
        // Fraction of max HP at which the fireball charge begins.
        private const double ChargeThreshold = 0.5;

        // How many of the boss's turns the charge takes before it unleashes.
        private const int ChargeTurns = 4;

        // Raw power of the unleashed fireball, added on top of the king's Magic stat.
        private const int FireballPower = 40;

        private bool _phaseStarted;
        private bool _charging;
        private int _chargeProgress;

        public LegendKing(string name, int maxHp, int maxMp,
                          int strength, int magic, int defense, int magicDefense, int speed)
            : base(name, maxHp, maxMp, strength, magic, defense, magicDefense, speed)
        {
        }

        /// <summary>
        /// Drives the king's turn. Below the threshold he charges, then unleashes the
        /// fireball; otherwise he trades normal blows.
        /// </summary>
        /// <param name="target">The actor he's fighting (the player).</param>
        public override void TakeTurn(Actor target)
        {
            // Enter the charge phase the first time he drops below the threshold.
            if (!_phaseStarted && CurrentHp <= MaxHp * ChargeThreshold)
            {
                _phaseStarted = true;
                _charging = true;
                _chargeProgress = 0;
                SceneHelpers.Narrate(
                    $"{Name} plants his feet and begins gathering a roiling sphere of fire. " +
                    "His guard drops as every ounce of his focus pours into the spell!");
                return;
            }

            if (_charging)
            {
                _chargeProgress++;

                if (_chargeProgress >= ChargeTurns)
                {
                    _charging = false;
                    int dealt = target.TakeDamage(Magic + FireballPower);
                    SceneHelpers.Narrate(
                        $"\"I dare you to overthrow me!\" {Name} unleashes O-Fireball. It engulfs " +
                        $"you for {dealt} damage.");
                }
                else
                {
                    SceneHelpers.Narrate(
                        $"The fireball swells in {Name}'s grasp, brighter and hotter. ");
                }
                return;
            }

            // Normal behaviour: alternate a heavy physical blow with a magic blast.
            if (_phaseStarted)
            {
                int dealt = target.TakeDamage(Magic + 4);
                SceneHelpers.Narrate($"{Name} hurls a bolt of dark flame for {dealt} damage.");
            }
            else
            {
                int dealt = target.TakeDamage(Strength);
                SceneHelpers.Narrate($"{Name} brings his blade down on you for {dealt} damage.");
            }
        }
    }
}
