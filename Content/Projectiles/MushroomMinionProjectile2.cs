using BoosterPackGlowingMushrooms.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Projectiles{

    public class MushroomMinionProjectile2 : ModProjectile{

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionShot[Type] = true;
        }

        public override void SetDefaults() {
			Projectile.width = 24; // The width of projectile hitbox
			Projectile.height = 24; // The height of projectile hitbox

			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.timeLeft = 80;
			Projectile.tileCollide = false;
		}

        public override void AI()
        {
			Lighting.AddLight(Projectile.position, TorchID.Mushroom);
			Projectile.velocity.X *= 0.92F;
			Projectile.velocity.Y *= 0.92F;

			Projectile.rotation += Projectile.velocity.Length() * 0.03F;

			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
			dust.noGravity = true;
			dust.scale *= 0.8f;

			Projectile.alpha += 3;
        }

        public override void OnKill(int timeLeft) {
			for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= 0.9f;
			} 
		}

        public override bool PreDraw(ref Color lightColor)
        {
			lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}