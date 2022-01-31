
using UnityEditor;
using UnityEngine;
//all for testing
using UnityEngine.InputSystem;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using System.Collections.Generic;
using ControllerVibration;

namespace ControllerVibrationTester
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
        private List<IVibrationPart> CurrentVibrateList = new List<IVibrationPart>();

        private void TestVibrate(List<IVibrationPart> newList)
        {
            if (CheckIfCanVibrate() == false) { return; }
            //this only takes a reference
            //CurrentVibrateList = newList;

            //this actually copies
            CurrentVibrateList = new List<IVibrationPart>(newList);
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
            foreach (IVibrationPart data in CurrentVibrateList)
            { yield return EditorCoroutineUtility.StartCoroutineOwnerless(IVibrate(data)); }

            //finishes the current list
            CurrentVibrateList.Clear();
        }


        /*Should only be called by vibrate manager*/
        private IEnumerator IVibrate(IVibrationPart data)
        {

            //initialises the vibrate (required for animated curves)
            data.Activate();

            float duration = data.GetPartDataDuration();

            //check if the data needs to be checked every frame
            bool repeat = data.UpdateFrame();

            //if it needs to repeat check
            if (repeat)
            {
                while (data.UpdateFrame())
                {
                    Vector2 strength = data.GetPartDataStrength();
                    Gamepad.current.SetMotorSpeeds(strength.x, strength.y);
                    //continues to the next frame
                    yield return null;
                }
            } //if it only needs to happen once
            else
            {
                Vector2 strength = data.GetPartDataStrength();
                Gamepad.current.SetMotorSpeeds(strength.x, strength.y);
                //completes the vibration duration
                yield return new EditorWaitForSeconds(duration);
            }
            //resets
            Gamepad.current.SetMotorSpeeds(0, 0);
        }
        #endregion
    }
}