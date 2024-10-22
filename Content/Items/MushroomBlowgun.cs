using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FirstMod.Content.Projectiles;
using FirstMod.Content.Buffs;
using Microsoft.Build.Execution;

namespace FirstMod.Content.Items{

    public class MushroomBlowgun : ModItem{

        public override void SetDefaults()
        {
			Item.width = 48; // Hitbox width of the item.
			Item.height = 48; // Hitbox height of the item.
			Item.scale = 1f;
			Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 25; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 25; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

			// The sound that this item plays when used.
			Item.UseSound = SoundID.Item64;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 12; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 1f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.
            

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 20f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = ItemID.Seed;

			Item.value = Item.buyPrice(silver: 80);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > 0.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, position, velocity * 0.2f, ProjectileID.SporeCloud, (int)((float)(damage) * 0.25f), knockback);
            return true;
        }

        public override void AddRecipes()
        {
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Blowpipe, 1);
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
        }
    }
}