using UnityEngine;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Curve Part")]
    public class VibrationCurve : IVibrationPart
    {
       [Header("X Axis = Duration: \nIs the longest curve.\n\nY Axis = Strength: \nShould be between 0 and 1\n")]
        public AnimationCurve leftVibeOverTime = AnimationCurve.Linear(0, 0, 0.3f, 1);
        public AnimationCurve rightVibeOverTime = AnimationCurve.Linear(0, 0, 0.3f, 1);


        private float duration = 0;
        private Vector2 strength = new Vector2(0f, 0f);

        //to check how long the update has gone
        private float timer = 0f;

        //the Unity editor stuff is to be able to test the animated curves in the inspector
#if UNITY_EDITOR
        private float startTime;
#endif

        public override void Activate()
        {
            timer = 0f;

#if UNITY_EDITOR
            startTime = Time.realtimeSinceStartup;
#endif


            //gets longer of both the curves 
            duration = leftVibeOverTime[leftVibeOverTime.length - 1].time;
            if (duration < rightVibeOverTime[rightVibeOverTime.length - 1].time)
            { duration = rightVibeOverTime[rightVibeOverTime.length - 1].time; }
        }

        //needs to loop so marked true
        public override bool UpdateFrame()
        {
            timer += Time.deltaTime;

#if UNITY_EDITOR
            timer = Time.realtimeSinceStartup - startTime;
#endif

            strength.x = leftVibeOverTime.Evaluate(timer);
            strength.y = rightVibeOverTime.Evaluate(timer);

            if (duration >= timer)
            {
                Debug.Log("Time not done");
                return true;
            }
            else
            {
                Debug.Log("Time done");
                return false;
            }

        }

        public override Vector2 GetPartDataStrength() { return strength; }

        public override float GetPartDataDuration() { return duration; }
    }
}