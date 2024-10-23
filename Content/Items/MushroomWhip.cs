using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BoosterPackGlowingMushrooms.Content.Projectiles;

namespace BoosterPackGlowingMushrooms.Content.Items{

    public class MushroomWhip : ModItem{

		public override void SetDefaults() {
			// This method quickly sets the whip's properties.
			// Mouse over to see its parameters.
			Item.DefaultToWhip(ModContent.ProjectileType<MushroomWhipProjectile>(), 12, 2, 4);
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(silver: 80);
		}

		public override bool MeleePrefix() {
			return true;
		}

        public override void AddRecipes(){
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 15);
			recipe.AddIngredient(ItemID.IronBar, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();

            recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 15);
			recipe.AddIngredient(ItemID.LeadBar, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
    }

}