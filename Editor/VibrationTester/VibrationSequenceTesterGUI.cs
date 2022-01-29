
using UnityEditor;
using UnityEngine;
//all for testing
using UnityEngine.InputSystem;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using System.Collections.Generic;

namespace ControllerVibration
{
    [CustomEditor(typeof(VibrationSequence))]
    public class VibrationSequenceTesterGUI : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            VibrationSequence VibeSequence = (VibrationSequence)target;

            if (GUILayout.Button("Test Sequence"))
            //{ sequence.Vibrate(); }
            { TestVibrate(VibeSequence.GetSequence()); }
        }


        #region This section is nearly identical to VibrateController.cs except it uses EditorCoroutines
        //the sequence to get off the scriptable object
        //the current list being executed
        private List<VibrationPart> CurrentVibrateList = new List<VibrationPart>();

        private void TestVibrate(List<VibrationPart> newList)
        {
            if (CheckIfCanVibrate() == false) { return; }
            //this only takes a reference
            //CurrentVibrateList = newList;

            //this actually copies
            CurrentVibrateList = new List<VibrationPart>(newList);
            EditorCoroutineUtility.StartCoroutineOwnerless(IVibrateManager());

        }

        private bool CheckIfCanVibrate()
        {

            if (CurrentVibrateList.Count > 0)
            {
                Debug.Log("already vibrating");
                return false;

            }
            else { return true; }
        }


        private IEnumerator IVibrateManager()
        {
            foreach (VibrationPart data in CurrentVibrateList)
            { yield return EditorCoroutineUtility.StartCoroutineOwnerless(IVibrate(data)); }

            //finishes the current list
            CurrentVibrateList.Clear();
        }


        /*Should only be called by vibrate manager*/
        private IEnumerator IVibrate(VibrationPart data)
        {
            Gamepad.current.SetMotorSpeeds(data.strength.x, data.strength.y);

            //completes the vibration duration
            yield return new EditorWaitForSeconds(data.duration);

            //resets
            Gamepad.current.SetMotorSpeeds(0, 0);
        }

        #endregion
    }
}