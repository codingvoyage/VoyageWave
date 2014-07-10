#region Using Statements
using System;
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

            // We add the floor the first so the rocks are on top of Tim
            EntityManager.Add(credits.Entity);
            //EntityManager.Add(floor);
            EntityManager.Add(sky);


        }


    }
}
