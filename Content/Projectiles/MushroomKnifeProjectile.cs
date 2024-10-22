using BoosterPackGlowingMushrooms.Content.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Projectiles{

    public class MushroomKnifeProjectile : ModProjectile{

		public override void SetDefaults() {
			Projectile.width = 12; // The width of projectile hitbox
			Projectile.height = 12; // The height of projectile hitbox

			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.timeLeft = 1200;
		}

        public override void AI()
        {
			Projectile.velocity.X *= 0.97F;
			Projectile.velocity.Y *= 0.97F;

			// Apply gravity after a quarter of a second
            Projectile.ai[0] += 1f;
			if (Projectile.ai[0] >= 15f) {
				Projectile.ai[0] = 15f;
				Projectile.velocity.Y += 0.9f;
			}

			Projectile.rotation += Projectile.velocity.X * 0.03F;

			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Confetti_Blue);
			dust.noGravity = true;
			dust.scale *= 0.8f;
        }

		int debuff1 = ModContent.BuffType<MushroomKnifeDebuff>();
		int debuff2 = ModContent.BuffType<MushroomKnifeDebuff2>();
		int debuff3 = ModContent.BuffType<MushroomKnifeDebuff3>();
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if(target.HasBuff(debuff1)){
				int d = target.FindBuffIndex(debuff1);
				target.DelBuff(d);
				target.AddBuff(debuff2, 600);
			}else if(target.HasBuff(debuff2)){
				int d = target.FindBuffIndex(debuff2);
				target.DelBuff(d);
				target.AddBuff(debuff3, 600);
			} else if(target.HasBuff(debuff3)){
				target.AddBuff(debuff3, 600);
			}else{
				target.AddBuff(debuff1, 600);
			}
        }

        public override void OnKill(int timeLeft) {
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Plays the basic sound most projectiles make when hitting blocks.
			for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Confetti_Blue);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= 0.9f;
			} 
		}
    }
}