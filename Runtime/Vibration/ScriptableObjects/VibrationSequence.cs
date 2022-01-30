using UnityEngine;
using System.Collections.Generic;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Sequence")]
    public class VibrationSequence : ScriptableObject
    {
        [SerializeReference]
        public List<IVibrationPart> sequence = new List<IVibrationPart>();



#if UNITY_EDITOR
        //returns the sequence to the vibrate tester
        public List<IVibrationPart> GetSequence()
        { return sequence; }
#endif
    }
}