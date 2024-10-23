using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace BoosterPackGlowingMushrooms.Content.Buffs{

    public class MushroomKnifeDebuff3 : ModBuff{

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.CanBeRemovedByNetMessage[Type] = true;
            
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Confetti_Blue);
			dust.noGravity = true;
			dust.velocity *= 1.5f;
			dust.scale *= 1.5f;

            dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Confetti_Blue);
            dust.noGravity = false;
			dust.scale *= 0.5f;
            
        }
    }
}