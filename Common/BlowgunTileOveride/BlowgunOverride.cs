using BoosterPackGlowingMushrooms.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace BoosterPackGlowingMushrooms.Common.BlowguntileOveride{

    public class BlowgunOverride : GlobalTile{


        
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if(type == TileID.Plants){
                if(Main.LocalPlayer.HasItem(ModContent.ItemType<MushroomBlowgun>())){
                    if(Main.rand.NextFloat() >= 0.5){
                        Item.NewItem(null, new Vector2(i, j) * 16, ItemID.Seed, 1);
                        
                    }
                }
            }
        }
    }
}