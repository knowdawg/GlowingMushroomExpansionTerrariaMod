using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace BoosterPackGlowingMushrooms.Content.Items.Armour
{
    [AutoloadEquip(EquipType.Body)]
    internal class MushroomBreastplate : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;

            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.pickSpeed -= 0.05f;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.CopperBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

            recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 20);
            recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
            recipe.AddIngredient(ItemID.TinBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}