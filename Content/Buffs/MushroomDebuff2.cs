using Terraria;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Buffs{

    internal class MushroomDebuff2 : ModBuff{

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.manaCost *= 0.9F;
            player.statLifeMax2 = (int)((float)(player.statLifeMax2) * 0.8F);
            player.statManaMax2 += 50;
        }
    }
}