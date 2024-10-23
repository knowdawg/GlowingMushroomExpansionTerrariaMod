using Terraria;
using Terraria.ModLoader;
namespace BoosterPackGlowingMushrooms.Content.Buffs{

    internal class AleSwordBuff : ModBuff{

        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true; //Won't be based on time
            Main.debuff[Type] = false;
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1 + (10 / 100F);
        }
    }
}