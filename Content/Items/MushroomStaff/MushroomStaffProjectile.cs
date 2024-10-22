using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Items.MushroomStaff{

    public class MushroomStaffProjectile : ModProjectile{

		public override void SetDefaults() {
			Projectile.width = 24;
			Projectile.height = 24;

            Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.timeLeft = 100;
		}


        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item43, Projectile.position);
        }

        float tickTimer = 0f;
        public override void AI()
        {
            if(!colided){
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

                if(Projectile.velocity.Length() > 2F){

                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
                    dust.noGravity = true;

                    Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
                    dust2.noGravity = true;
                    dust2.scale = 1.5f;
                    dust2.velocity = -Projectile.velocity / 2;
                }
            }else{
                tickTimer += 1.0f;
                Projectile.rotation += 0.02f * tickTimer;
                Projectile.velocity.Y = -1;
                Projectile.velocity.X = 0;

                Projectile.scale += 0.001f * tickTimer;

				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.MushroomTorch);
				dust.noGravity = true;
				dust.velocity *= 5.0f;
				dust.scale *= 1f;
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Lava);
				dust2.noGravity = true;
				dust2.velocity *= 5.0f;
				dust2.scale *= 0.5f;

                if(tickTimer >= 30f){
                    Projectile.timeLeft = 0;
                }
            }
        }

        bool colided = false;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if(!colided){
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Projectile.timeLeft = 60;
                colided = true;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.timeLeft = 60;
            colided = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return !colided;
        }

        public override void OnKill(int timeLeft) {
            IEntitySource source = Projectile.GetSource_FromThis();
			SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
			for (int i = 0; i < 20; i++)
			{
                Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Lava);
				dust2.noGravity = true;
				dust2.velocity *= 3.0f;
				dust2.scale *= 1f;
			}
            Projectile.NewProjectile(source, Projectile.position, new Vector2(0), ProjectileID.DD2ExplosiveTrapT3Explosion, Projectile.damage, Projectile.knockBack, Projectile.owner);
		}
    }
}