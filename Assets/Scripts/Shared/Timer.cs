using System;

namespace Ulf
{
    /// <summary>
    /// Calculate remaining and elapsed time. Foundation for time effect mechanic
    /// </summary>
    public class Timer
    {
        public Action<float> OnTimeAction;
        public Action OnTimeOver;
        protected float remainTime, startTime;
        protected DateTime lastUpdateTime;
        public DateTime LastUpdateTime => lastUpdateTime;
        /// <summary>
        /// start time minus remain time (сколько прошло времени)
        /// </summary>
        public float ElapsedTime
        {
            get
            {
                return startTime > remainTime ? startTime - remainTime : 0;
            }
            private set { }
        }
        public float RemainingTime
        {
            get { return remainTime; }
            set
            {
                if (startTime == 0)
                    startTime = value;

                remainTime = value;

                OnTimeAction?.Invoke(remainTime);   

                if (remainTime <= 0)
                {
                    remainTime = 0;
                    OnTimeOver?.Invoke();
                    //timeOverDelegate?.Invoke();
                }

                lastUpdateTime = DateTime.Now;
            }
        }
        public Timer()
        {
            startTime = remainTime = 0;
            //timeOverDelegate = null;
        }

        public Timer(float remainTime_)
        {
            startTime = remainTime = remainTime_;
            //timeOverDelegate = null;
            lastUpdateTime = DateTime.Now;
        }
        /// <summary>
        /// when remain time is 0 -> invoke TimeOverDelegate
        /// </summary>
        /// <param name="remainTime_"></param>
        /// <param name="overDelegate"></param>
        //public Timer(float remainTime_, Action overDelegate)
        //{
        //    startTime = remainTime = remainTime_;
        //    timeOverDelegate = overDelegate;
        //}
    }
}