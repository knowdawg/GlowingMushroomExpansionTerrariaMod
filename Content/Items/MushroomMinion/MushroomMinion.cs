using Iced.Intel;
using Microsoft.Build.Evaluation;
using Microsoft.Xna.Framework;
using Mono.Cecil.Cil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Items.MushroomMinion
{
	// This file contains all the code necessary for a minion
	// - ModItem - the weapon which you use to summon the minion with
	// - ModBuff - the icon you can click on to despawn the minion
	// - ModProjectile - the minion itself

	// It is not recommended to put all these classes in the same file. For demonstrations sake they are all compacted together so you get a better overview.
	// To get a better understanding of how everything works together, and how to code minion AI, read the guide: https://github.com/tModLoader/tModLoader/wiki/Basic-Minion-Guide
	// This is NOT an in-depth guide to advanced minion AI

	public class MushroomMinionBuff : ModBuff
	{
		public override void SetStaticDefaults() {
			Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
			Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
			
		}

		public override void Update(Player player, ref int buffIndex) {
			// If the minions exist reset the buff time, otherwise remove the buff from the player
			if (player.ownedProjectileCounts[ModContent.ProjectileType<MushroomMinionProjectile>()] > 0) {
				player.buffTime[buffIndex] = 18000;
			}
			else {
				player.DelBuff(buffIndex);
				buffIndex--;
			}
		}
	}

	public class MushroomMinionItem : ModItem
	{
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
		}

		public override void SetDefaults() {
			Item.damage = 10;
			Item.knockBack = 3f;
			Item.mana = 5; // mana cost
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.HoldUp; // how the player's arm moves when using the item
			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item44; // What sound should play when using the item

			// These below are needed for a minion weapon
			Item.noMelee = true; // this item doesn't do any melee damage
			Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
			Item.buffType = ModContent.BuffType<MushroomMinionBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ModContent.ProjectileType<MushroomMinionProjectile>(); // This item creates the minion projectile
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
			position = Main.MouseWorld;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			// Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
			var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
			projectile.originalDamage = Item.damage;

			// Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
			return false;
		}

	}

	// This minion shows a few mandatory things that make it behave properly.
	// Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	// If the player targets a certain NPC with right-click, it will fly through tiles to it
	// If it isn't attacking, it will float near the player with minimal movement
	public class MushroomMinionProjectile : ModProjectile
	{

        static long summonCount = 0;
        int summonIndex = 0;
        int summonID = ModContent.ProjectileType<MushroomMinionProjectile>();
		public override void SetStaticDefaults() {
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 1;
			// This is necessary for right-click targeting
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

			Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		
            
        }

		public sealed override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.tileCollide = false; // Makes the minion go through tiles freely

			// These below are needed for a minion weapon
			Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
			Projectile.minion = true; // Declares this as a minion (has many effects)
			Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
			Projectile.minionSlots = 1f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles    
        }

        public override void OnSpawn(IEntitySource source)
        {
            updatePos();
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
            }
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles() {
			return false;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage() {
			return false;
		}

        int animTimer = 30;
        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        public override void AI() {

			Player owner = Main.player[Projectile.owner];

			if (!CheckActive(owner)) {
				return;
			}

            if(Main.rand.NextFloat() > 0.7f){
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
                Main.dust[d].noGravity = true;
            }

            updatePos();

            SearchForTargets(owner, out bool foundTarget, out Vector2 targetCenter);
            Attack(foundTarget, targetCenter);



            if(animTimer == 1){
                    for (int i = 0; i < 10; i++)
                {
                    Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
                }
            }

            if(animTimer <= 30){
                Vector2 dir = new Vector2((float)Math.Cos(Projectile.rotation - Math.PI/2), (float)Math.Sin(Projectile.rotation - Math.PI/2));
                Projectile.position += (float)Math.Cos(animTimer * Math.PI / 60) * dir * 20f;
            }else if(animTimer == 31){
                animTimer += Main.rand.Next(0, 10);
            }else{
                Projectile.position.X += (float)Math.Sin((animTimer-30) * Math.PI / 100) * 4;
            }
            animTimer++;
		}

        
        private void updatePos(){
            Player p = Main.player[Projectile.owner];
            int numOfSummon = p.ownedProjectileCounts[summonID];
            if(numOfSummon == 0){
                numOfSummon = 1;
            }
            summonCount += 1;
            summonIndex = (int)(summonCount % numOfSummon) + 1;

            Vector2 offSet = new Vector2(0);
            double angle = (((Math.PI) * summonIndex) / (1 + numOfSummon)) - (Math.PI / 2);
            offSet.X = (float)Math.Sin(angle);
            offSet.Y = -(float)Math.Cos(angle);
            offSet *= 50;

            Projectile.position = offSet + p.position - new Vector2(8, 0);
        }
        

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner) {
			if (owner.dead || !owner.active) {
				owner.ClearBuff(ModContent.BuffType<MushroomMinionBuff>());

				return false;
			}

			if (owner.HasBuff(ModContent.BuffType<MushroomMinionBuff>())) {
				Projectile.timeLeft = 2;
			}

			return true;
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out Vector2 targetCenter) {
            float maxDis = 500;
            float closest = maxDis;
            foundTarget = false;
            targetCenter = new Vector2(0);

            if (owner.HasMinionAttackTargetNPC){
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float dis = Vector2.Distance(npc.Center, Projectile.Center);

                if(dis <= maxDis){
                    foundTarget = true;
                    targetCenter = npc.Center;
                }
            }

            if(!foundTarget){
                foreach (var npc in Main.ActiveNPCs) {
                    if (npc.CanBeChasedBy()) {
                        float dis = Vector2.Distance(npc.Center, Projectile.Center);
                        //bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);

                        if(dis < closest){
                            closest = dis;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
		}

        int tickTimer = 0;
		private void Attack(bool foundTarget, Vector2 targetCenter) {
            tickTimer += 1;
			if (foundTarget) {
                float targetRotation = targetCenter.AngleTo(Projectile.position) - (float)(Math.PI / 2);
				Projectile.rotation = Projectile.rotation.AngleLerp(targetRotation, 0.1f);

                if(tickTimer >= 60){
                    Vector2 dir = Projectile.position - targetCenter;
                    dir.Normalize();
                    dir *= -100;

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position + new Vector2(16, 16), dir, ProjectileID.Mushroom, Projectile.damage, Projectile.knockBack, Projectile.owner);

                    SoundEngine.PlaySound(SoundID.NPCDeath9, Projectile.position);

                    tickTimer = Main.rand.Next(-30, 30);
                    animTimer = 0;
                }
            }
		}
	}
}