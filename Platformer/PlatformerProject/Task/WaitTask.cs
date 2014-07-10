using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PlatformerProject.Task
{
    class WaitTask : BaseTask
    {
        int waitMilliseconds;
        float elapsedMilliseconds;

        public WaitTask(CallBack callBack, int milliseconds)
            : base(callBack)
        {
            waitMilliseconds = milliseconds;
            elapsedMilliseconds = 0;
        }

        public override void Update(TimeSpan gameTime)
        {
            elapsedMilliseconds += gameTime.Milliseconds;

            if (elapsedMilliseconds > waitMilliseconds)
            {
                this.taskStatus = Status.DEAD;
            }
        }
    }
}
