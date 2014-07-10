using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo.IronLua;
using WaveEngine.Common.Math;
using System.Diagnostics;

namespace PlatformerProject.Task
{
    class TaskManager
    {

        LinkedList<BaseTask> taskList;
        private LuaBridge bridge;

        public TaskManager(LuaBridge bridge)
        {
            this.bridge = bridge;
            taskList = new LinkedList<BaseTask>();
        }

        //Update all tasks...
        //Note - this is terrible programming because LinkedList shouldn't be
        //used like this
        public void Update(TimeSpan gameTime)
        {

            var node = taskList.First;
            while (node != null)
            {
                var nextNode = node.Next;
                var task = node.Value;
                if (task.getStatus() != Status.DEAD) 
                {
                    task.Update(gameTime);
                }
                else
                {
                    task.Destroy();
                    taskList.Remove(node);
                }
                node = nextNode;
            }

        }

        public int Count
        {
            get { return taskList.Count; }
        }

        //Task constructors. These should actually really go in some Factory
        //class, which would be more appropriate.

        public void Move(LuaThread t, TimBehavior myTim, int moveLength, int xDir, int yDir)
        {
            MoveTask task = new MoveTask(
                new CoroutineCallBack(bridge, t),
                myTim, 
                moveLength, 
                Vector2.Normalize(new Vector2(xDir, yDir))
            );
            taskList.AddLast(task);

        }

        public void Wait(LuaThread t, int milliseconds)
        {
            WaitTask wait = new WaitTask(
                new CoroutineCallBack(bridge, t), milliseconds);
            taskList.AddLast(wait);


        }
    }
}
