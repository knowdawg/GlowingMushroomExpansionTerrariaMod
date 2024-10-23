using BoosterPackGlowingMushrooms.Content.Items;
using BoosterPackGlowingMushrooms.Content.Items.MushroomMinion;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Common.GlobalNPCs{

    public class GlobalNPCs : GlobalNPC{

        
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot){
            if (npc.type == NPCID.SporeBat || npc.type == NPCID.SporeSkeleton || npc.type == NPCID.AnomuraFungus || npc.type == NPCID.ZombieMushroom || npc.type == NPCID.ZombieMushroomHat || npc.type == NPCID.MushiLadybug) {
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MushroomSpellBook>(), 40, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MushroomFlail>(), 40, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MushroomGun>(), 40, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MushroomMinionItem>(), 40, 1, 1));
                    npcLoot.Add(ItemDropRule.Common(ItemID.GlowingMushroom, 2, 1, 3));
            }
        }
    }
}