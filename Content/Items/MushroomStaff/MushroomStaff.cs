using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using FirstMod.Content.Projectiles;

namespace FirstMod.Content.Items.MushroomStaff{

    public class MushroomStaff : ModItem{

        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
			Item.width = 48;
			Item.height = 48;
			Item.value = Item.buyPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Blue;
			Item.damage = 14;
			Item.knockBack = 5;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = Item.useAnimation = 30;
			Item.mana = 12;
			Item.DamageType = DamageClass.Magic;
			Item.channel = true;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = false;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.shoot = ModContent.ProjectileType<MushroomStaffShaft>();
			Item.shootSpeed = 10f;
        }

		int StaffProjIndex = ModContent.ProjectileType<MushroomStaffShaft>();
		int OrbProjIndex = ModContent.ProjectileType<MushroomStaffOrb>();
		int SpiralProjIndex = ModContent.ProjectileType<MushroomStaffSpiral>();
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			velocity = new Vector2(0f);

            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

		int ct = 0;

        public override void HoldItem(Player player)
        {
            ct -= 1;
			if(player.channel == true && player.ownedProjectileCounts[SpiralProjIndex] >= 1){
				ct = 10;
			}
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if(ct <= 0){
				if (player.ownedProjectileCounts[StaffProjIndex] < 1){
					Projectile.NewProjectile(source, position, velocity, StaffProjIndex, damage, 3, player.whoAmI);
				}
				if (player.ownedProjectileCounts[SpiralProjIndex] < 1){
					Projectile.NewProjectile(source, position, velocity, SpiralProjIndex, damage, 3, player.whoAmI);
				}
				if (player.ownedProjectileCounts[OrbProjIndex] < 1){
					Projectile.NewProjectile(source, position, velocity, OrbProjIndex, damage, 3, player.whoAmI);
				}
			}
			return false;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddIngredient(ItemID.Sapphire, 1);
			recipe.AddIngredient(ItemID.GoldBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 10);
			recipe.AddIngredient(ItemID.Sapphire, 1);
			recipe.AddIngredient(ItemID.PlatinumBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }






	internal class MushroomStaffSpiral : ModProjectile{
		public override void SetDefaults()
        {
			Projectile.width = 24;
			Projectile.height = 24;

			Projectile.alpha = 255;

            Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 999999;
        }

		int tickTimer = 0;
        public override void AI()
        {
			tickTimer += 2;

			Player p = Main.player[Projectile.owner];

			if(Main.mouseLeftRelease && p.channel != true){
				Projectile.timeLeft = 0;
			}

			if(p.direction == 1){
				Projectile.position = p.position + new Vector2(13, -21);
			}else{
				Projectile.position = p.position + new Vector2(-17, -21);
			}

			if(tickTimer > 100){
				Projectile.scale = 1.0f;
				Projectile.rotation -= 0.05f;
			}else{
				Projectile.rotation -= (float)(tickTimer) / 300f;
				Projectile.alpha -= 3;
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenBlood);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= (float)(tickTimer) / 100f;
			}

			if(tickTimer == 100){
				Projectile.scale = 2.0f;
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
	}







	internal class MushroomStaffOrb : ModProjectile{
		public override void SetDefaults()
        {
			Projectile.width = 14;
			Projectile.height = 14;

            Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 999999;
        }

		int tickTimer = 0;
        public override void AI()
        {
			tickTimer += 2;

			Player p = Main.player[Projectile.owner];
			p.itemAnimation = ItemUseStyleID.Shoot;

			if(Main.mouseLeftRelease && p.channel != true){
				Vector2 dir = Main.MouseWorld;
				dir -= Projectile.position;
				dir.Normalize();
				dir *= 10;

				if(tickTimer >= 100){
					Projectile.NewProjectile(null, Projectile.position + new Vector2(0, 24), dir * 2, ModContent.ProjectileType<MushroomStaffProjectile>(), Projectile.damage * 2, Projectile.knockBack * 2, p.whoAmI);
				}else if (tickTimer >= 1){
					Projectile.NewProjectile(null, Projectile.position + new Vector2(0, 24), dir, ProjectileID.SapphireBolt, Projectile.damage, Projectile.knockBack, p.whoAmI);
				}
				
				Projectile.timeLeft = 0;
			}

			if(p.direction == 1){
				Projectile.position = p.position + new Vector2(18, -16);
			}else{
				Projectile.position = p.position + new Vector2(-12, -16);
			}

			if(tickTimer > 100){
				Projectile.scale = 1.0f;
				Projectile.rotation += 0.05f;
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
				dust.noGravity = true;
				dust.velocity *= 0.0f;
				dust.scale = 0.5f;
			}else{
				Projectile.rotation += (float)(tickTimer) / 200f;

				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= (float)(tickTimer) / 100f;
			}

			if(tickTimer == 100){
				SoundEngine.PlaySound(SoundID.Item4, Projectile.position);
				Projectile.scale = 2.0f;
				for (int i = 0; i < 30; i++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
					dust.noGravity = true;
					dust.velocity *= 5.0f;
					dust.scale = 1.5f;

					Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GreenBlood);
					dust2.noGravity = true;
					dust2.velocity *= 4.0f;
					dust2.scale = 1.0f;
				}
			}
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
	}







	internal class MushroomStaffShaft : ModProjectile{

        public override void SetDefaults()
        {
			Projectile.width = 48;
			Projectile.height = 48;

            Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 999999;
        }

        public override void AI()
        {
			Player p = Main.player[Projectile.owner];
			//p.itemAnimation = ItemUseStyleID.Shoot;

			if(Main.mouseLeftRelease && p.channel != true){
				Projectile.timeLeft = 0;
			}

			if(p.direction == 1){
				Projectile.position = p.position + new Vector2(-6, -8);
				Projectile.rotation = -(float)(Math.PI / 6);
			}else{
				Projectile.position = p.position + new Vector2(-22, -8);
				Projectile.rotation = -(float)(Math.PI / 3);
			}
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
			overPlayers.Add(index);
        }

    }
}