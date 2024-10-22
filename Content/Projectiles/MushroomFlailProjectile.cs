using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Projectiles{

    public class MushroomFlailProjectile : ModProjectile{

		public override void SetStaticDefaults() {
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true; //flail visual thing
		}

		public override void SetDefaults() {
			Projectile.netImportant = true; // This ensures that the projectile is synced when other players join the world.
			Projectile.width = 16; // The width of your projectile
			Projectile.height = 16; // The height of your projectile
			Projectile.friendly = true;
			Projectile.penetrate = -1; // Infinite pierce
			Projectile.DamageType = DamageClass.Melee; // Deals melee damage
			Projectile.scale = 0.8f;
			Projectile.usesLocalNPCImmunity = true; // Used for hit cooldown changes in the ai hook
			Projectile.localNPCHitCooldown = 10; // This facilitates custom hit cooldown logic

			// Here we reuse the flail projectile aistyle and set the aitype to the Sunfury. These lines will get our projectile to behave exactly like Sunfury would. This only affects the AI code, you'll need to adapt other code for the other behaviors you wish to use.
			Projectile.aiStyle = ProjAIStyleID.Flail;
			AIType = ProjectileID.Mace;

			// These help center the projectile as it rotates since its hitbox and scale doesn't match the actual texture size
			DrawOffsetX = -6;
			DrawOriginOffsetY = -6;
		}

    }
}