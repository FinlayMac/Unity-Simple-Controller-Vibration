using UnityEngine;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Curve Part")]
    public class VibrationCurve : IVibrationPart
    {
        [Header("Duration is defined by the longest curve")]
        public AnimationCurve leftMotorVibrationOverTime = AnimationCurve.Linear(0, 0, 0.3f, 1);
        public AnimationCurve rightMotorVibrationOverTime = AnimationCurve.Linear(0, 0, 0.3f, 1);



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
            duration = leftMotorVibrationOverTime[leftMotorVibrationOverTime.length - 1].time;
            if (duration < rightMotorVibrationOverTime[rightMotorVibrationOverTime.length - 1].time)
            { duration = rightMotorVibrationOverTime[rightMotorVibrationOverTime.length - 1].time; }
        }

        //needs to loop so marked true
        public override bool UpdateFrame()
        {
            timer += Time.deltaTime;

#if UNITY_EDITOR
            timer = Time.realtimeSinceStartup - startTime;
#endif

            strength.x = leftMotorVibrationOverTime.Evaluate(timer);
            strength.y = rightMotorVibrationOverTime.Evaluate(timer);

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