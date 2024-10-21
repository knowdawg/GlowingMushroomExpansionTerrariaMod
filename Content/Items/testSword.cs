using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class testSword : ModItem
	{
		int hits = 0;
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.FirstMod.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 64;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			
			Item.value = Item.buyPrice(silver: 80);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.shoot = ProjectileID.Ale;
			Item.shootSpeed = 10.0F;
		}

		public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2) {
				Item.useTime = 15;
				Item.useAnimation = 15;
				Item.useStyle = ItemUseStyleID.Thrust;
				Item.shoot = ProjectileID.Ale;
			}else{
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shoot = ProjectileID.None;
			}

			return true;
		}
        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2) {
				Item.useTime = 15;
				Item.useAnimation = 15;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shoot = ProjectileID.Ale;
			}else{
				Item.damage = 20;
				Item.useTime = 20;
				Item.useAnimation = 20;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.shoot = ProjectileID.None;	
			}
            return true;
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            if (player.altFunctionUse == 2) {
				return false;
			}else{
				return null;
			}
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if(hits >= 1){
				int d = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Sandnado);
			}
        }

        public override void HoldItem(Player player)
        {
			player.AddBuff(BuffID.Tipsy, 1);
        }

        public override void UpdateInventory(Player player)
        {
            if(Item.Name != player.HeldItem.Name){
				if(player.HasBuff(aleSwordBuffIndex)){
					player.ClearBuff(aleSwordBuffIndex);
				}
				hits = 0;
			}
        }

        int aleSwordBuffIndex = ModContent.BuffType<Content.Buffs.AleSwordBuff>();
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			player.AddBuff(aleSwordBuffIndex, 999999);
			
			hits += 1;
			if(hits > 5){
				hits = 5;
			}
        }


        //Double Ale Dmg
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			damage *= 2;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(hits >= 1){
				hits -= 1;
				Random r = new Random();

				Vector2 randVec = Vector2.Zero;

				float randNum = (r.NextInt64() % 200) / 100.0F;
				randNum -= 1.0F;
				randVec.X = randNum;

				randNum = (r.NextInt64() % 200) / 100.0F;
				randNum -= 1.0F;
				randVec.Y = randNum;

				Projectile.NewProjectile(source, position, velocity + randVec, type, damage, knockback, player.whoAmI);

				if(hits <= 0){
					if(player.HasBuff(aleSwordBuffIndex)){
						player.ClearBuff(aleSwordBuffIndex);
					}
				}
			}
			
			return false;
        }

        public override void ModifyItemScale(Player player, ref float scale)
        {
            if(player.altFunctionUse == 2){
				scale = 0.0F;
			}else{
				scale = 1.0f;
			}
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ale, 3);
			recipe.AddIngredient(ItemID.Topaz, 5);
			recipe.AddIngredient(ItemID.ShadowScale, 5);
			recipe.AddTile(TileID.Kegs);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Ale, 3);
			recipe.AddIngredient(ItemID.Topaz, 5);
			recipe.AddIngredient(ItemID.TissueSample, 5);
			recipe.AddTile(TileID.Kegs);
			recipe.Register();
		}
	}
}
