using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace ControllerVibration
{
    public class VibrateController : MonoBehaviour
    {

        //the current list being executed
        private List<VibrationPart> CurrentVibrateList = new List<VibrationPart>();



        #region Enable / disable vibration
        public void StopVibration()
        { Gamepad.current.PauseHaptics(); }

        public void ResumeVibration()
        { Gamepad.current.ResumeHaptics(); }

        #endregion


        public void Vibrate(List<VibrationPart> newList)
        {
            if (CheckIfCanVibrate() == false) { return; }

            //this only takes a reference
            //CurrentVibrateList = newList;

            //this actually copies
            CurrentVibrateList = new List<VibrationPart>(newList);
            StartCoroutine(IVibrateManager());
        }

        private bool CheckIfCanVibrate()
        {
            //Debug.Log("if already vibrating");
            if (CurrentVibrateList.Count > 0) { return false; }
            else { return true; }
        }


        private IEnumerator IVibrateManager()
        {
            //Debug.Log("starting pattern");
            foreach (VibrationPart data in CurrentVibrateList)
            { yield return StartCoroutine(IVibrate(data)); }

            //finishes the current list
            CurrentVibrateList.Clear();
            // Debug.Log("finished pattern");
        }


        /*Should only be called by vibrate manager*/
        private IEnumerator IVibrate(VibrationPart data)
        {
            Gamepad.current.SetMotorSpeeds(data.strength.x, data.strength.y);

            //completes the vibration duration
            yield return new WaitForSeconds(data.duration);

            //resets
            Gamepad.current.SetMotorSpeeds(0, 0);
        }

    }
}