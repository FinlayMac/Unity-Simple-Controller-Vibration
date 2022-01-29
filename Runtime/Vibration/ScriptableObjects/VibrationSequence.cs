using UnityEngine;
using System.Collections.Generic;

namespace ControllerVibration
{
    [CreateAssetMenu(menuName = "Sciptable Objects/Vibration/Sequence")]
    public class VibrationSequence : ScriptableObject
    {
        public List<VibrationPart> sequence = new List<VibrationPart>();



        #if UNITY_EDITOR
        //returns the sequence to the vibrate tester
        public List<VibrationPart> GetSequence()
        { return sequence; }
        #endif
    }
}