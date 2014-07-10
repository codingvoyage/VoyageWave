using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo.IronLua;

namespace PlatformerProject
{

    public class LuaBridge
    {
        private Lua lua;
        private LuaGlobal environment;

        public LuaBridge()
        {
            lua = new Lua();
            environment = lua.CreateEnvironment();
        }

        /// <summary>
        /// Runs all the code from a file
        /// </summary>
        /// <param name="fileName">Name of the file, relative to Platformer directory</param>
        public void DoFile(string fileName)
        {
            environment.DoChunk(fileName);
        }

        /// <summary>
        /// Get/set values from the Lua Environment
        /// </summary>
        /// <param name="variableName">Variable name in the Lua State</param>
        /// <returns>A variable returned in object from. Needs casting. </returns>
        public object this[string variableName]
        {
            get
            {
                return environment[variableName];
            }
            set
            {
                environment[variableName] = value;
            }
        }

        public LuaResult CallFunction(string functionName, params object[] args)
        {
            Delegate function = environment[functionName] as Delegate;
            object result = function.DynamicInvoke(args);
            return (LuaResult)result;
        }

        /// <summary>
        ///     The entry point for coroutines. Begins a thread, then
        ///     returns the results from first coroutine.yield(a, b, ...)
        /// </summary>
        /// <param name="functionName">
        ///     The name of the function to create a Coroutine from
        /// </param>
        /// <param name="args">
        ///     Variables to be passed to the Lua Function as parameters
        /// </param>
        /// <returns>
        ///     A package of the LuaThread handle ([0]) and first LuaResult ([1])
        /// 
        ///     First element in LuaResult is whether it succeeded, so if
        ///     coroutine.yield(a, b, c, d), then result.Count = 5, where
        ///     result[1] = a, result[2] = b, result[3] = c, result[4] = d
        /// </returns>
        public object[] BeginCoroutine(string functionName, params object[] args)
        {

            Delegate function = environment[functionName] as Delegate;

            Debug.Print(functionName);

            LuaThread t = new LuaThread(function);

            //Convention: the first parameter will be the thread's handle; copy varargs over.
            int numArgs = args.Length; 
            object[] parameters = new object[numArgs + 1];
            parameters[0] = t;
            for (int i = 0; i < args.Length; i++)
            {
                parameters[i+1] = args[i];
            }

            Debug.Print("Parameter length: " + parameters.Length);

            object[] returnPackage = new Object[2];
            returnPackage[0] = t;
            returnPackage[1] = ResumeCoroutine(t, parameters);

            return returnPackage;
        }

        /// <summary>
        ///     Resumes execution of a coroutine.
        ///     Returns the results from coroutine.yield(a, b, ...)
        /// </summary>
        /// <param name="t">
        ///     The handle to the LuaThread to resume.
        /// </param>
        /// <returns>
        ///     First element in LuaResult is whether it succeeded, so if
        ///     coroutine.yield(a, b, c, d), then result.Count = 5, where
        ///     result[1] = a, result[2] = b, result[3] = c, result[4] = d
        /// </returns>
        public LuaResult ResumeCoroutine(LuaThread t, params object[] args)
        {
            return t.resume(args);
        }

    }

}
