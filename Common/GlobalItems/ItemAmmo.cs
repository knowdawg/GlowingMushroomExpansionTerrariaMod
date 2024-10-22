using BoosterPackGlowingMushrooms.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BoosterPackGlowingMushrooms.Common.GlobalItems{

    public class ModifyDefaults : GlobalItem{
        public override void SetDefaults(Item Item){
            if(Item.type == ItemID.GlowingMushroom)
            {
                Item.damage = 4; // The damage for projectiles isn't actually 12, it actually is the damage combined with the projectile and the item together.
                Item.DamageType = DamageClass.Ranged;
                Item.width = 24;
                Item.height = 24;
                Item.maxStack = Item.CommonMaxStack;
                Item.consumable = true; // This marks the item as consumable, making it automatically be consumed when it's used as ammunition, or something else, if possible.
                Item.knockBack = 0.5f;
                Item.value = 10;
                Item.rare = ItemRarityID.Green;
                Item.shootSpeed = 16f; // The speed of the projectile.
                Item.ammo = Item.type; // The ammo class this ammo belongs to.
                Item.shoot = ModContent.ProjectileType<MushroomGrenadeProjectile>();
                Item.notAmmo = true;
            }
        }

        public override bool CanShoot(Item item, Player player)
        {
            if(item.type == ItemID.GlowingMushroom){
                if(item.Name == player.HeldItem.Name){
                    return false;
			    }
                return true;
            }
            return base.CanShoot(item, player);
        }
    }
}