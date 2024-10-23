using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Buffs
{
	public class MushroomWhipTagDebuff : ModBuff
	{
		public static readonly int TagDamage = 500;

		public override void SetStaticDefaults() {
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			// Other mods may check it for different purposes.
			BuffID.Sets.IsATagBuff[Type] = true;
		}
	}
}