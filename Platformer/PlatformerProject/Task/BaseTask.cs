using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerProject.Task
{   
    enum Status
    {
        NEW, ACTIVE, SUSPENDED, DEAD
    }

    abstract class BaseTask
    {
        protected Status taskStatus;
        protected CallBack callBack;

        public BaseTask(CallBack callBack)
        {
            this.callBack = callBack;
            taskStatus = Status.NEW;
        }

        public abstract void Update(TimeSpan gameTime);

        public virtual void Destroy()
        {
            callBack.CallBackAction();
        }

        public Status getStatus()
        {
            return taskStatus;
        }

    }


    
}
