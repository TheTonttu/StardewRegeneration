using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;

namespace Tonttu.StardewValleyGame.Mods.StardewRegeneration {
    public class StardewRegenMod : Mod {

        private double _lastStaminaRegenTime = 0;
        private double _lastHealthRegenTime = 0;
        private StardewRegenConfig _config;

        public override void Entry(IModHelper helper) {
            _config = helper.ReadConfig<StardewRegenConfig>();
            SaveEvents.AfterLoad += SaveEvents_AfterLoad;
        }

        private void SaveEvents_AfterLoad(object sender, EventArgs e) {
            GameEvents.UpdateTick += GameEvents_UpdateTick;
        }

        private void GameEvents_UpdateTick(object sender, EventArgs e) {
            StardewValley.Farmer player = Game1.player;
            if (player == null) { return; }

            double currentTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            DoHealthRegen(player, currentTime);
            DoStaminaRegen(player, currentTime);
        }

        private void DoHealthRegen(StardewValley.Farmer player, double currentTime) {
            if (!_config.HealthRegenEnabled) { return; }
            if (IsInCooldown(currentTime, _lastHealthRegenTime, _config.HealthRegenInterval)) { return; }

            _lastHealthRegenTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            if (player.health < player.maxHealth) {
                player.health = Math.Min(player.health + _config.HealthRegenAmount, player.maxHealth);
            }
        }

        private void DoStaminaRegen(StardewValley.Farmer player, double currentTime) {
            if (!_config.StaminaRegenEnabled) { return; }
            if (IsInCooldown(currentTime, _lastStaminaRegenTime, _config.StaminaRegenInterval)) { return; }

            _lastStaminaRegenTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            if (player.stamina < player.maxStamina) {
                player.stamina = Math.Min(player.stamina + _config.StaminaRegenAmount, player.maxStamina);
            }
        }

        private bool IsInCooldown(double currentTime, double lastRegenTime, double regenInterval) {
            return currentTime < lastRegenTime + regenInterval;
        }
    }
}
