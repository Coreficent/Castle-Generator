namespace Coreficent.Controller
{
    using UnityEngine;

    public class TimeController
    {
        private float savedTime = 0.0f;
        private float timeLeft = 0.0f;

        public float TimePassed
        {
            get { return Time.time - savedTime; }
        }

        public bool Reached
        {
            get { return TimePassed > timeLeft; }
        }

        public TimeController() { }

        public void Reset()
        {
            savedTime = Time.time;
        }

        public bool Passed(float time)
        {
            return TimePassed > time;
        }

        public float Progress(float time)
        {
            return TimePassed / time;
        }

        public void SetTime(float time)
        {
            timeLeft = time;
        }
    }
}
