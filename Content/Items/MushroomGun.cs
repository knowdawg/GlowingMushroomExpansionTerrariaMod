using System;
using BoosterPackGlowingMushrooms.Content.Projectiles;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace BoosterPackGlowingMushrooms.Content.Items{

    public class MushroomGun : ModItem{

		public override void SetDefaults() {
			// Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

			// Common Properties
			Item.width = 48; // Hitbox width of the item.
			Item.height = 48; // Hitbox height of the item.
			Item.scale = 1f;
			Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.

			// Use Properties
			Item.useTime = 100; // The item's use time in ticks (60 ticks == 1 second.)
			Item.useAnimation = 100; // The length of the item's use animation in ticks (60 ticks == 1 second.)
			Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
			Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.

			// The sound that this item plays when used.
			Item.UseSound = SoundID.Item61;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged; // Sets the damage type to ranged.
			Item.damage = 14; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
			Item.knockBack = 1f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
			Item.noMelee = true; // So the item's animation doesn't do damage.
            

			// Gun Properties
			Item.shoot = ProjectileID.PurificationPowder; // For some reason, all the guns in the vanilla source have this.
			Item.shootSpeed = 16f; // The speed of the projectile (measured in pixels per frame.)
			Item.useAmmo = ItemID.GlowingMushroom;

			Item.value = Item.buyPrice(silver: 80);
		}

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() > 0.75;
        }

        public override Vector2? HoldoutOffset() {
			return new Vector2(0f, -0f);
		}
    }
}