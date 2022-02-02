using UnityEngine;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Simple Part")]
    public class VibrationPart : IVibrationPart
    {
        [Min(0f)]
        public float duration = 0;
        [Range(0f, 1f)]
        public float LeftVibeStrength;
        [Range(0f, 1f)]
        public float RightVibeStrength;
        private Vector2 strength;

        public override void Activate() { }

        //no need to loop so marked false
        public override bool UpdateFrame() { return false; }

        public override Vector2 GetPartDataStrength()
        {
            strength = new Vector2(LeftVibeStrength, RightVibeStrength);
            return strength;
        }

        public override float GetPartDataDuration() { return duration; }

    }
}