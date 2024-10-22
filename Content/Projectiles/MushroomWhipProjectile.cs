using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Content.Projectiles{

    public class MushroomWhipProjectile : ModProjectile{
        public override void SetStaticDefaults() {
			// This makes the projectile use whip collision detection and allows flasks to be applied to it.
			ProjectileID.Sets.IsAWhip[Type] = true;
		}

        public override void SetDefaults() {
			// This method quickly sets the whip's properties.
			Projectile.DefaultToWhip();

			// use these to change from the vanilla defaults
			Projectile.WhipSettings.Segments = 10;
			// Projectile.WhipSettings.RangeMultiplier = 1f;
		}
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            int r = Main.rand.Next(0, 4);
            if(r == 0){
                target.AddBuff(BuffID.Confused, 120);
            }
			Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
			Projectile.damage = (int)(Projectile.damage * 0.5f); // Multihit penalty. Decrease the damage the more enemies the whip hits.
		}


        //Below code is necesary to draw the whip
        private void DrawLine(List<Vector2> list) {
			Texture2D texture = TextureAssets.FishingLine.Value;
			Rectangle frame = texture.Frame();
			Vector2 origin = new Vector2(frame.Width / 2, 2);

			Vector2 pos = list[0];
			for (int i = 0; i < list.Count - 1; i++) {
				Vector2 element = list[i];
				Vector2 diff = list[i + 1] - element;

				float rotation = diff.ToRotation() - MathHelper.PiOver2;
				Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
				Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

				Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

				pos += diff;
			}
		}


        public override bool PreDraw(ref Color lightColor) {
			List<Vector2> list = new List<Vector2>();
			Projectile.FillWhipControlPoints(Projectile, list);

			DrawLine(list);

			Main.DrawWhip_WhipBland(Projectile, list);

            return false;
        }
    }
}