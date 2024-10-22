using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Items.Armour
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class MushroomLegs : ModItem
	{

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
		}

        public override void UpdateEquip(Player player)
        {
            player.pickSpeed -= 0.05f;
        }

		public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 15);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemID.CopperBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

            recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 15);
            recipe.AddRecipeGroup("IronBar", 8);
            recipe.AddIngredient(ItemID.TinBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}