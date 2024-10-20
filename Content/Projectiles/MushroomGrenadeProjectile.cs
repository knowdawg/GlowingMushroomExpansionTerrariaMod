using System;
using FirstMod.Content.Buffs;
using Iced.Intel;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace FirstMod.Content.Projectiles{

    public class MushroomGrenadeProjectile : ModProjectile{

		public override void SetDefaults() {
			Projectile.width = 24;
			Projectile.height = 24;

            Projectile.penetrate = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.timeLeft = 60;
		}

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon) {
                Projectile.velocity.X = -oldVelocity.X;
            }

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon) {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }

        public override void AI()
        {
			Projectile.velocity.X *= 0.93F;
			Projectile.velocity.Y *= 0.93F;

			Projectile.rotation += Projectile.velocity.Length() * 0.05F;

            if(Projectile.velocity.Length() > 2F){

                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Confetti_Blue);
                dust.noGravity = true;
            }
        }

        public override void OnKill(int timeLeft) {
            IEntitySource source = Projectile.GetSource_FromThis();
			SoundEngine.PlaySound(SoundID.Item107, Projectile.position);
			for (int i = 0; i < 6; i++)
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Confetti_Blue);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= 2f;
                
                double a = Math.PI / 3;
                a *= i;
                float r = Main.rand.NextFloat();
                Vector2 v = new Vector2();
                v.X = (float)(Math.Cos(a)) * r;
                v.Y = (float)(Math.Sin(a)) * r;
                Projectile.NewProjectile(source, Projectile.position, v, ProjectileID.SporeGas, (int)(Projectile.damage * 0.5F), 0, Projectile.whoAmI);
			}
		}
    }
}