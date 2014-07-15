using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;
using WaveEngine.Framework.Diagnostic;

namespace PlatformerProject
{
    public class Factory
    {
        private Scene scene;
        private LuaBridge bridge;
        private int count;
        private System.Random rand;

        public Factory(Scene scene, LuaBridge bridge)
        {
            this.scene = scene;
            this.bridge = bridge;

            rand = new System.Random();
            count = 2;
        }

        public void MakeTim()
        {
            var tim = new Entity("Tim" + count)
                .AddComponent(new Transform2D()
                {
                    X = (float)(rand.NextDouble() * WaveServices.Platform.ScreenWidth),
                    Y = (float)(rand.NextDouble() * WaveServices.Platform.ScreenHeight),
                    //X = WaveServices.Platform.ScreenWidth / 2,
                    //Y = WaveServices.Platform.ScreenHeight /2 ,
                    Origin = new Vector2(0.5f, 1),
                    XScale = 0.4f,
                    YScale = 0.4f
                })
                .AddComponent(new Sprite("Content/TimSpriteSheet.wpk"))
                .AddComponent(Animation2D.Create<TexturePackerGenericXml>("Content/TimSpriteSheet.xml")
                    .Add("Idle", new SpriteSheetAnimationSequence() { First = 1, Length = 22, FramesPerSecond = 11 })
                    .Add("Running", new SpriteSheetAnimationSequence() { First = 23, Length = 27, FramesPerSecond = 27 }))
                .AddComponent(new AnimatedSpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new TimBehavior());

            

            scene.EntityManager.Add(tim);
            var anim2D = tim.FindComponent<Animation2D>();
            anim2D.Play(true);

            bridge.BeginCoroutine("timBehavior", tim.FindComponent<TimBehavior>());

            count++;
        }

        public void MakeFire()
        {
            //var fireParticleEntity = new Entity("fire")
            //     .AddComponent(new Transform3D())
            //     .AddComponent(fireParticle)
            //     .AddComponent(new MaterialsMap(new BasicMaterial("Content/particleFire.wpk", DefaultLayers.Additive) { VertexColorEnabled = true }))
            //     .AddComponent(new ParticleSystemRenderer());

        }

    }
}
