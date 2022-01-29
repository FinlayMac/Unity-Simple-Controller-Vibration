using UnityEngine;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Part")]
    public class VibrationPart : ScriptableObject
    {
        public float duration = 0;
        public Vector2 strength = new Vector2(0, 0);

    }
}