using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Items.Trinkets{

    public class MushroomSporeSack : ModItem{

        public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.buyPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}

        int tickTimer = 0;
        public override void UpdateAccessory(Player player, bool hideVisual) {
			tickTimer += 1;

            if(tickTimer % 60 == 0){
                Vector2 p = new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-50, 50));
                Projectile.NewProjectile(player.GetSource_FromThis(), player.position + p, new Vector2(0), ProjectileID.TruffleSpore, 10, 1, player.whoAmI);
            }
		}

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Chain, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
        }
    }
}