using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Items.Trinkets{

    public class StandingStillCritBuff : ModItem{

        public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.buyPrice(silver: 50);
			Item.rare = ItemRarityID.Blue;
			Item.accessory = true;
		}

        int standingStillTimer = 0;
        public override void UpdateAccessory(Player player, bool hideVisual) {
            if(player.velocity.Length() < 1){
                standingStillTimer++;
            }else{
                standingStillTimer = 0;
            }

            if(standingStillTimer >= 120){
                player.GetCritChance<GenericDamageClass>() += 10;
                player.GetDamage<GenericDamageClass>() += 0.1f;
                
                if(standingStillTimer % 10 == 0){
                    Dust.NewDust(player.position, player.width, player.height, DustID.MushroomTorch, 0, 0);
                    // Vector2 p = new Vector2(Main.rand.Next(-player.width / 2, player.width / 2), Main.rand.Next(-player.height / 2, player.height / 2));
                    // Projectile.NewProjectile(player.GetSource_FromThis(), player.position + p, new Vector2(0), ProjectileID.Mushroom, 0, 0, player.whoAmI);
                }
            }

            if(standingStillTimer == 120){
                for (int i = 0; i < 30; i++)
                {
                    SoundEngine.PlaySound(SoundID.Item4, player.position);
                    int d = Dust.NewDust(player.position, player.width, player.height, DustID.MushroomTorch, Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                    Main.dust[d].noGravity = true;
                }
            }
		}

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 5);
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
        }
    }
}