using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace ControllerVibration
{
    public class VibrateController : MonoBehaviour
    {

        //the current list being executed
        private List<IVibrationPart> CurrentVibrateList = new List<IVibrationPart>();



        #region Enable / disable vibration
        public void StopVibration()
        { Gamepad.current.PauseHaptics(); }

        public void ResumeVibration()
        { Gamepad.current.ResumeHaptics(); }

        #endregion


        public void Vibrate(List<IVibrationPart> newList)
        {
            if (CheckIfCanVibrate() == false) { return; }

            //this only takes a reference
            //CurrentVibrateList = newList;

            //this actually copies
            CurrentVibrateList = new List<IVibrationPart>(newList);
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
            foreach (IVibrationPart data in CurrentVibrateList)
            { yield return StartCoroutine(IVibrate(data)); }

            //finishes the current list
            CurrentVibrateList.Clear();
            // Debug.Log("finished pattern");
        }


        /*Should only be called by vibrate manager*/
        private IEnumerator IVibrate(IVibrationPart data)
        {
            //initialises the vibrate (required for animated curves)
            data.Activate();

            float duration = data.GetPartDataDuration();

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
            }
            //if it only needs to happen once
            else
            {

                Vector2 strength = data.GetPartDataStrength();
                Gamepad.current.SetMotorSpeeds(strength.x, strength.y);
                //completes the vibration duration
                yield return new WaitForSeconds(duration);
            }
            //resets
            Gamepad.current.SetMotorSpeeds(0, 0);
        }

    }
}