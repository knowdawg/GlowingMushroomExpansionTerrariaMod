using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FirstMod.Content.Items.Armour
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Head value here will result in TML expecting a X_Head.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Head)]
	public class MushroomHelmet : ModItem
	{

		public static LocalizedText SetBonusText { get; private set; }

		public override void SetStaticDefaults() {
			// If your head equipment should draw hair while drawn, use one of the following:
			// ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false; // Don't draw the head at all. Used by Space Creature Mask
			// ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
			// ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
			// ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true;

		}

		public override void SetDefaults() {
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ItemRarityID.Blue; // The rarity of the item
			Item.defense = 3; // The amount of defense the item will give when equipped
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            bool bodyMatch = body.type == ModContent.ItemType<MushroomBreastplate>();
            bool legMatch = legs.type == ModContent.ItemType<MushroomLegs>();
            return bodyMatch && legMatch;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "You glow in the dark";

            
            Lighting.AddLight(player.position, new Vector3(0.3f, 0.3f, 1));
        }

        public override void UpdateEquip(Player player)
        {
            player.pickSpeed -= 0.05f;
        }

        public override void AddRecipes() {
            Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddIngredient(ItemID.CopperBar, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

            recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddIngredient(ItemID.TinBar, 6);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}