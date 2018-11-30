#region copyright
// StardewRegeneration - StardewRegenMod.cs
// Copyright © 2017 Tonttu and StardewRegeneration contributors
// 
// StardewRegeneration is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// StardewRegeneration is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion copyright

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
            Farmer player = Game1.player;
            bool isPaused = !Game1.shouldTimePass();
            if (player == null || isPaused) { return; }

            double currentTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            DoHealthRegen(player, currentTime);
            DoStaminaRegen(player, currentTime);
        }

        private void DoHealthRegen(Farmer player, double currentTime) {
            if (!_config.HealthRegenEnabled) { return; }
            if (IsInCooldown(currentTime, _lastHealthRegenTime, _config.HealthRegenInterval)) { return; }

            _lastHealthRegenTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            if (player.health < player.maxHealth) {
                player.health = Math.Min(player.health + _config.HealthRegenAmount, player.maxHealth);
            }
        }

        private void DoStaminaRegen(Farmer player, double currentTime) {
            if (!_config.StaminaRegenEnabled) { return; }
            if (IsInCooldown(currentTime, _lastStaminaRegenTime, _config.StaminaRegenInterval)) { return; }

            _lastStaminaRegenTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;
            if (player.Stamina < player.MaxStamina) {
                player.Stamina = Math.Min(player.stamina + _config.StaminaRegenAmount, player.MaxStamina);
            }
        }

        private bool IsInCooldown(double currentTime, double lastRegenTime, double regenInterval) {
            return currentTime < lastRegenTime + regenInterval;
        }
    }
}
