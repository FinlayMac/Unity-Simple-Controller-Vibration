using UnityEngine;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Simple Part")]
    public class VibrationPart : IVibrationPart
    {
        public float duration = 0;
        public Vector2 strength = new Vector2(0, 0);

        public override void Activate() { }

        //no need to loop so marked false
        public override bool UpdateFrame() { return false; }

        public override Vector2 GetPartDataStrength() { return strength; }

        public override float GetPartDataDuration() { return duration; }

    }
}