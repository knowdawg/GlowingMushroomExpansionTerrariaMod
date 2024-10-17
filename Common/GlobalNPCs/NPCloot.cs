using FirstMod.Content.Items;
using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod.Common.GlobalNPCs{

    public class GlobalNPCs : GlobalNPC{

        
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot){
            if (npc.type == NPCID.SporeBat || npc.type == NPCID.SporeSkeleton) {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MushroomSpellBook>(), 20, 1, 1));
                    
            }
        }
    }
}