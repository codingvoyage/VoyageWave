using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using WaveEngine.Common.Math;

namespace PlatformerProject.Task
{
    class MoveTask : BaseTask
    {
        float moveDistance;
        float elapsedDistance;
        Vector2 direction;
        //Velocity in pixels per second
        float velocity;
        TimBehavior behavior;

        
        public MoveTask(CallBack callBack, TimBehavior behavior, int moveLength, 
            Vector2 direction) : base (callBack)
        {
            this.behavior = behavior;
            elapsedDistance = 0;
            this.direction = direction;
            this.moveDistance = (float)moveLength;
            velocity = 3.0f;
        }

        public override void Update(TimeSpan gameTime)
        {
            float dx = direction.X * velocity;
            float dy = direction.Y * velocity;
            Vector2 ds = new Vector2(dx, dy);
            behavior.move(ds);
            elapsedDistance += ds.Length();
            //Debug.Print("elapsed: " + elapsedDistance + ". goal: " + moveDistance);
            if (elapsedDistance > moveDistance)
            {
                //Debug.Print("DEAD");
                this.taskStatus = Status.DEAD;
            }
        }

        public override void Destroy()
        {
            behavior.move(new Vector2(0, 0));
            //Base behavior is to perform the CallBackAction.
            base.Destroy();
        }

    }

}
