using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BoosterPackGlowingMushrooms.Content.Projectiles;
using BoosterPackGlowingMushrooms.Content.Buffs;

namespace BoosterPackGlowingMushrooms.Content.Items{

    public class MushroomSword : ModItem{

		int projectileIndex = ModContent.ProjectileType<MushroomKnifeProjectile>();
        public override void SetDefaults()
        {
            Item.damage = 14;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			
			Item.value = Item.buyPrice(silver: 30);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;

			Item.shootSpeed = 25.0F;
        }


        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			damage = damage / 3;
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        int debuff1 = ModContent.BuffType<MushroomKnifeDebuff>();
		int debuff2 = ModContent.BuffType<MushroomKnifeDebuff2>();
		int debuff3 = ModContent.BuffType<MushroomKnifeDebuff3>();

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone){
            if(target.HasBuff(debuff1)){
				var p = Projectile.NewProjectile(null, target.position + new Vector2(12, -30F), new Vector2(0F, 0F), ProjectileID.DD2ExplosiveTrapT1Explosion, Item.damage * 2, 0);
				int b = target.FindBuffIndex(debuff1);
				target.DelBuff(b);
			}else if(target.HasBuff(debuff2)){
				Projectile.NewProjectile(null, target.position + new Vector2(12, -28F), new Vector2(0F, 0F), ProjectileID.DD2ExplosiveTrapT2Explosion, Item.damage * 3, 0);
				
				int b = target.FindBuffIndex(debuff2);
				target.DelBuff(b);
			} else if(target.HasBuff(debuff3)){
				Projectile.NewProjectile(null, target.position + new Vector2(12, -24F), new Vector2(0F, 0F), ProjectileID.DD2ExplosiveTrapT3Explosion, Item.damage * 4, 0);
				
				int b = target.FindBuffIndex(debuff3);
				target.DelBuff(b);
			}else{
				return;
			}
			Terraria.Audio.SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, target.position);
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if (player.altFunctionUse == 2) {
			
			}else{
				Dust dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Lava);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= 1.0f;
			}
        }

        public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2) {
				Item.shoot = projectileIndex;
                Item.noUseGraphic = true;
			}else{
				Item.shoot = ProjectileID.None;
                Item.noUseGraphic = false;
			}

			return true;
		}
        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2) {
				Item.shoot = projectileIndex;
                Item.noUseGraphic = true;
			}else{
				Item.shoot = ProjectileID.None;
                Item.noUseGraphic = false;
			}
            return true;
        }


        public override bool? CanHitNPC(Player player, NPC target)
        {
            if (player.altFunctionUse == 2) {
				return false;
			}else{
				if(target.HasBuff(debuff1)){
					Item.knockBack = 5;
				}else if(target.HasBuff(debuff2)){
					Item.knockBack = 7;
				} else if(target.HasBuff(debuff3)){
					Item.knockBack = 10;
				}else{
					Item.knockBack = 3;
				}
				return null;
			}
        }

        public override bool AltFunctionUse(Player player) {
			return true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 20);
			recipe.AddIngredient(ItemID.Ruby, 1);
			recipe.AddIngredient(ItemID.GoldBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();

			recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 20);
			recipe.AddIngredient(ItemID.Ruby, 1);
			recipe.AddIngredient(ItemID.PlatinumBar, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
    }
}