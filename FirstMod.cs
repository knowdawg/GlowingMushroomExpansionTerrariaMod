using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace FirstMod
{

	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class FirstMod : Mod
	{
        // public override void Load()
        // {
		// 	if (Main.netMode != NetmodeID.Server)
		// 	{
		// 		Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("Shaders/Trippy.fx", AssetRequestMode.ImmediateLoad)); // The path to the compiled shader file.
		// 		Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
		// 		Filters.Scene["Shockwave"].Load();
		// 	}
        // }
    }
}
