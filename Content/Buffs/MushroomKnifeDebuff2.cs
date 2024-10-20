using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.ComponentModel;

namespace FirstMod.Content.Buffs{

    public class MushroomKnifeDebuff2 : ModBuff{

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            //BuffID.Sets.CanBeRemovedByNetMessage[Type] = true;
            
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Confetti_Blue);
			dust.noGravity = true;
			dust.velocity *= 1.5f;
			dust.scale *= 1f;
            
        }
    }
}