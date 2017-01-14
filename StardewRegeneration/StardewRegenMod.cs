using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tonttu.StardewValley.Mods.StardewRegeneration {
    public class StardewRegenMod : Mod {

        private double _lastStaminaRegenTime = 0;
        private double _lastHealthRegenTime = 0;
        private StardewRegenConfig _config;

        public override void Entry(IModHelper helper) {
            _config = helper.ReadConfig<StardewRegenConfig>();
            PlayerEvents.LoadedGame += PlayerEvents_LoadedGame;
        }

        private void PlayerEvents_LoadedGame(object sender, EventArgsLoadedGameChanged e) {
            GameEvents.UpdateTick += GameEvents_UpdateTick;
        }

        private void GameEvents_UpdateTick(object sender, EventArgs e) {
            Farmer player = Game1.player;
            if (player == null) { return; }

            double currentTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            DoHealthRegen(player, currentTime);
            DoStaminaRegen(player, currentTime);
        }

        private void DoHealthRegen(Farmer player, double currentTime) {
            if (!_config.HealthRegenEnabled) { return; }
            if (IsInCooldown(currentTime, _lastHealthRegenTime, _config.HealthRegenInterval)) { return; }

            _lastHealthRegenTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            if (player.health < player.maxStamina) {
                player.health = Math.Min(player.health + _config.HealthRegenAmount, player.maxHealth);
            }
        }

        private void DoStaminaRegen(Farmer player, double currentTime) {
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
