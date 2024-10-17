using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.CompilerServices.SymbolWriter;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FirstMod.Content.Items{

    public class MushroomSpellBook : ModItem{
        
        int mushroomDebuffIndex1 = ModContent.BuffType<Content.Buffs.MushroomDebuff>();
        int mushroomDebuffIndex2 = ModContent.BuffType<Content.Buffs.MushroomDebuff2>();
        int mushroomDebuffIndex3 = ModContent.BuffType<Content.Buffs.MushroomDebuff3>();
        int projectilesToFire = 0;
        EntitySource_ItemUse_WithAmmo s;
        Vector2 shootPos = Vector2.Zero;
        Vector2 shootVel = Vector2.Zero;
        int projDmg;
        float projKB;
        
        public override void SetDefaults()
        {
            Item.damage = 10;
			Item.DamageType = DamageClass.Magic;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 3;
            Item.mana = 10;
			
			Item.value = Item.buyPrice(silver: 35);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item21;
			Item.autoReuse = true;

			Item.shoot = ProjectileID.Mushroom;
			Item.shootSpeed = 50.0F;
        }

        int tickTimer = 0;
        int tickTimerLimit = 5;
        Random r = new Random();
        public override void HoldItem(Player player)
        {
            tickTimer += 1;
            tickTimerLimit = Item.useTime / 10;
            if(projectilesToFire >= 1 && tickTimer >= tickTimerLimit){
                float rn = (float)(r.Next() % 100F) / 5F;
                rn -= 10;

                float rn2 = (float)(r.Next() % 50F) / 5.0F;

                Projectile.NewProjectile(s, player.position, shootVel + new Vector2(rn, rn2), ProjectileID.Mushroom, projDmg, projKB, player.whoAmI);
                projectilesToFire -= 1;
                tickTimer = 0;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse != 2) {
                s = source;
                shootPos = position;
                shootVel = velocity;
                projDmg = damage;
                projKB = knockback;

                projectilesToFire = 4;
                if(player.HasBuff(mushroomDebuffIndex1)){
                    projectilesToFire += 1;
                }else if(player.HasBuff(mushroomDebuffIndex2)){
                    projectilesToFire += 2;
                } else if(player.HasBuff(mushroomDebuffIndex3)){
                    projectilesToFire += 3;
                }
            }
            return false;
        }

        public override bool CanUseItem(Player player) {
			if (player.altFunctionUse == 2) {
				Item.useTime = 20;
				Item.useAnimation = 20;
                Item.mana = 0;
				Item.useStyle = ItemUseStyleID.EatFood;
                Item.UseSound = SoundID.Item2;
			}else{
				Item.useTime = 40;
				Item.useAnimation = 40;
                Item.mana = 10;
				Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = SoundID.Item21;
			}
			return true;
		}
        public override bool? UseItem(Player player)
        {
			if (player.altFunctionUse == 2) {
				Item.useTime = 20;
				Item.useAnimation = 20;
                Item.mana = 0;
				Item.useStyle = ItemUseStyleID.EatFood;
                Item.UseSound = SoundID.Item2;

                if(player.HasBuff(mushroomDebuffIndex1)){
                    player.ClearBuff(mushroomDebuffIndex1);
                    player.AddBuff(mushroomDebuffIndex2, 1800);
                }else if(player.HasBuff(mushroomDebuffIndex2)){
                    player.ClearBuff(mushroomDebuffIndex2);
                    player.AddBuff(mushroomDebuffIndex3, 1800);
                } else if(player.HasBuff(mushroomDebuffIndex3)){
                    player.AddBuff(mushroomDebuffIndex3, 1800);
                }else{
                    player.AddBuff(mushroomDebuffIndex1, 1800);
                }

			}else{
				Item.useTime = 40;
				Item.useAnimation = 40;
                Item.mana = 10;
				Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = SoundID.Item21;
			}
			return true;
        }

        public override bool? CanHitNPC(Player player, NPC target)
        {
            return false;
        }

        public override bool AltFunctionUse(Player player) {
			return true;
		}

        public override void AddRecipes(){
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 20);
			recipe.AddIngredient(ItemID.MudBlock, 10);
			recipe.AddIngredient(ItemID.SilverBar, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();

            recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.GlowingMushroom, 20);
			recipe.AddIngredient(ItemID.MudBlock, 10);
			recipe.AddIngredient(ItemID.TungstenBar, 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
    }
}