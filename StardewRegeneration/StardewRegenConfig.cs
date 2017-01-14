using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tonttu.StardewValley.Mods.StardewRegeneration {
    public class StardewRegenConfig {
        public bool HealthRegenEnabled { get; set; }
        public int HealthRegenAmount { get; set; }
        /// <summary>
        /// Health regeneration interval in seconds. 
        /// </summary>
        public float HealthRegenInterval { get; set; }

        public bool StaminaRegenEnabled { get; set; }
        public int StaminaRegenAmount { get; set; }
        /// <summary>
        /// Stamina regeneration interval in seconds.
        /// </summary>
        public float StaminaRegenInterval { get; set; }

        public StardewRegenConfig() {
            HealthRegenEnabled = true;
            HealthRegenAmount = 1;
            HealthRegenInterval = 5;

            StaminaRegenEnabled = true;
            StaminaRegenAmount = 1;
            StaminaRegenInterval = 2;
        }
    }
}
