#region copyright
// StardewRegeneration - StardewRegenConfig.cs
// 
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion copyright

namespace Tonttu.StardewValleyGame.Mods.StardewRegeneration {
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
