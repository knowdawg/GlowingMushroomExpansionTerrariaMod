using System;
using FirstMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod.Content.Items{

    public class MushroomFlail : ModItem{

        public override void SetStaticDefaults(){
			// Make flail say it does more dmg than it actualy does. Flail Stuff! Rack your brain if confused!
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 2f;
		}


        public override void SetDefaults() {
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 45;
			Item.useTime = 45;
			Item.knockBack = 6.75f; // The knockback of your flail, this is dynamically adjusted in the projectile code.
			Item.width = 20; // Hitbox width of the item.
			Item.height = 20; // Hitbox height of the item.
			Item.damage = 9;
			Item.crit = 7; // Critical damage chance %
			Item.scale = 1.0f;
			Item.noUseGraphic = true; // This makes sure the item does not get shown when the player swings his hand
			Item.shoot = ModContent.ProjectileType<MushroomFlailProjectile>(); // The flail projectile
			Item.shootSpeed = 12f;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(silver: 50);
			Item.DamageType = DamageClass.MeleeNoSpeed;
			Item.channel = true;
			Item.noMelee = true; // This makes sure the item does not deal damage from the swinging animation
		}

		int tickTimer = 0;
		EntitySource_ItemUse_WithAmmo s;
		bool sIsSet = false;
        public override void HoldItem(Player player)
        {
			if(player.channel && sIsSet == true){
				tickTimer += 1;
				if(tickTimer >= 30){
					double rVal = (double)(Main.rand.Next());
					float rVal2 = Main.rand.NextFloat();
					float xDisp = (float)(Math.Sin(rVal) * 100) * rVal2;
					float yDisp = (float)(Math.Cos(rVal) * 100) * rVal2;
					Projectile.NewProjectile(s, player.position + new Vector2(xDisp, yDisp), new Vector2(0F, 0F), ProjectileID.TruffleSpore, Item.damage, 0f);
					tickTimer = 0;
				}
			}else{
				sIsSet = false;
			}
            base.HoldItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			sIsSet = true;
			s = source;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

		
    }

}