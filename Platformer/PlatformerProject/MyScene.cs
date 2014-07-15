#region Using Statements
using System;
using System.Diagnostics;

using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.UI;
using WaveEngine.Components.Cameras;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;
using WaveEngine.Framework.Diagnostic;
using WaveEngine.Components.Particles;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Materials;
#endregion

/*
 * Sample Platformer Project
 * Based on the online getting started tutorial from Wave Engine
 */

namespace PlatformerProject
{
    public class MyScene : Scene
    {

        private Factory f;

        public void SetFactory(Factory f)
        {
            this.f = f;
        }

        protected override void CreateScene()
        {
            
            // Set to true the diagnostic value
            WaveServices.ScreenContextManager.SetDiagnosticsActive(true);

            RenderManager.BackgroundColor = Color.CornflowerBlue;


            var credits = new TextBlock()
            {
                Text = "A test of Lua Coroutines via C# - Edmund",
                Margin = new Thickness(10, 10, 0, 0),
                Foreground = Color.Red
            };

            var sky = new Entity("Sky")
                .AddComponent(new Sprite("Content/Sky.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new Transform2D()
                {
                    Origin = new Vector2(0.5f, 1),
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = WaveServices.Platform.ScreenHeight
                });

            //var floor = new Entity("Floor")
            //    .AddComponent(new Sprite("Content/Floor.wpk"))
            //    .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
            //    .AddComponent(new Transform2D()
            //    {
            //        Origin = new Vector2(0.5f, 1),
            //        X = WaveServices.Platform.ScreenWidth / 2,
            //        Y = WaveServices.Platform.ScreenHeight
            //    });

            // Tim
            f.MakeTim();

            var camera = new FreeCamera("Camera", 
                new Vector3(50, 50, 50), 
                Vector3.Zero);


            ParticleSystem3D fireParticle = new ParticleSystem3D()
            {
                NumParticles = 18,
                MinLife = TimeSpan.FromSeconds(0.2f),
                MaxLife = TimeSpan.FromSeconds(0.7f),
                LocalVelocity = new Vector3(0.0f, 0.6f, 0.0f),
                RandomVelocity = new Vector3(0.1f, 0.0f, 0.0f),
                MinSize = 3,
                MaxSize = 8,
                MinRotateSpeed = 0.1f,
                MaxRotateSpeed = -0.1f,
                EndDeltaScale = 0.0f,
                EmitterSize = new Vector2(2),
                EmitterShape = ParticleSystem3D.Shape.Circle,
            };

            
            var fireParticleEntity = new Entity("fire")
                 .AddComponent(new Transform3D())
                 .AddComponent(fireParticle)
                 .AddComponent(new MaterialsMap(new BasicMaterial("Content/bloo.wpk", DefaultLayers.Additive) { VertexColorEnabled = true }))
                 .AddComponent(new ParticleSystemRenderer3D());

            EntityManager.Add(camera);

            EntityManager.Add(fireParticleEntity);
            string s = fireParticleEntity.ToString();


            // We add the floor the first so the rocks are on top of Tim
            EntityManager.Add(credits.Entity);
            //EntityManager.Add(floor);
            EntityManager.Add(sky);


        }

    }
}
