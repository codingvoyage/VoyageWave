#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Framework;
using WaveEngine.Common.Input;
using WaveEngine.Framework.Services;
using PlatformerProject.Task;
#endregion

namespace PlatformerProject
{
    public class Game : WaveEngine.Framework.Game
    {
        TaskManager taskManager;
        MyScene s;
        LuaBridge b;
        Factory f;
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);

            //Bind Lua with C#
            b = new LuaBridge();
            b.DoFile("Content/luabridge.lua");

            //TaskManager
            taskManager = new TaskManager(b);

            b["taskManager"] = taskManager;
            b["scene"] = s;

            //Scene.
            s = new MyScene();
            f = new Factory(s, b);
            s.SetFactory(f);
            b["factory"] = f;

            b.BeginCoroutine("spawner");

            ScreenContext screenContext = new ScreenContext(s);
            WaveServices.ScreenContextManager.To(screenContext);
        }

        public override void UpdateFrame(TimeSpan gameTime)
        {

            var keyboard = WaveServices.Input.KeyboardState;
            if (keyboard.Right == ButtonState.Pressed)
            {
                s.EntityManager.Remove("Tim2");
                s.EntityManager.Remove("fire");
                //currentState = AnimState.Right;
            }

            taskManager.Update(gameTime);
            base.UpdateFrame(gameTime);
        }


    }
}
