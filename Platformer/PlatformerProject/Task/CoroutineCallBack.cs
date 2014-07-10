using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo.IronLua;

namespace PlatformerProject.Task
{
    class CoroutineCallBack : CallBack
    {
        //Coroutine-continuation Callback
        private LuaThread callBack;
        private LuaBridge bridge;

        public CoroutineCallBack(LuaBridge bridge, LuaThread callBack)
        {
            this.bridge = bridge;
            this.callBack = callBack;
        }

        public void CallBackAction(params object[] args)
        {
            bridge.ResumeCoroutine(callBack, args);
        }

    }
}
