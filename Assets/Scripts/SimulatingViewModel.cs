using Assets.Scripts.Helpers;
using Assets.Scripts.MetaData;
using Assets.Scripts.MetaData.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Assets.Scripts
{
    public class SimulatingViewModel : MonoBehaviour
    {
        private void Start()
        {
            UpdateBlurValue(PlayerPrefs.GetFloat(PlayerPreferences.BlurLevel));
            UpdateTunnelValue(PlayerPrefs.GetFloat(PlayerPreferences.TunnelLevel));
            UpdateDelay(PlayerPrefs.GetInt(PlayerPreferences.DelayLevel));
            UpdateMotionBlur(PlayerPrefs.GetInt(PlayerPreferences.MotionBlur));
            UpdateRedColorDistortion(PlayerPrefs.GetInt(PlayerPreferences.RedColorDistortion));
            UpdateRandomEffects(PlayerPrefs.GetInt(PlayerPreferences.Randomization));
        }

        private void UpdateBlurValue(float value)
        {
            if (value > 0)
            {
                var blurValueText = GameObject.Find(SimulatingSubstancesControls.BlurLevelText).GetComponent<Text>();
                blurValueText.text = value.ToString();
                var cameraLeftBlur =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<BlurOptimized>();
                var cameraRightBlur =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<BlurOptimized>();
                cameraLeftBlur.blurSize = value;
                cameraRightBlur.blurSize = value;
                UiHelper.UpdateToggle(SimulatingSubstancesControls.BlurToggle);
            }
        }

        private void UpdateTunnelValue(float value)
        {
            if (value > 0)
            {
                var tunnelValueText = GameObject.Find(SimulatingSubstancesControls.TunnelLevelText).GetComponent<Text>();
                tunnelValueText.text = value.ToString();
                var cameraLeftTunnel =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<AlcoholTiltShift>();
                var cameraRightTunnel =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<AlcoholTiltShift>();
                cameraLeftTunnel.blurArea = value;
                cameraRightTunnel.blurArea = value;
                UiHelper.UpdateToggle(SimulatingSubstancesControls.TunnelToggle);
            }
        }

        private void UpdateDelay(float value)
        {
            if (value > 0)
            {
                var delayLeft = GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<Delay>();
                var delayRight = GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<Delay>();
                delayLeft.enabled = true;
                delayRight.enabled = true;
                UiHelper.UpdateToggle(SimulatingSubstancesControls.DelayToggle);
            }
        }

        private void UpdateMotionBlur(int value)
        {
            if (value > 0)
            {
                var motionBlurLeft =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<CameraMotionBlur>();
                var motionBlurRight =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraRight).GetComponent<CameraMotionBlur>();
                motionBlurLeft.enabled = true;
                motionBlurRight.enabled = true;
                UiHelper.UpdateToggle(SimulatingSubstancesControls.MotionBlurToggle);
            }
        }

        private void UpdateRedColorDistortion(int value)
        {
            if (value > 0)
            {
                var colorCorrectionCurvesLeft =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraLeft).GetComponent<ColorCorrectionCurves>();
                var colorCorrectionCurvesRight =
                    GameObject.Find(SimulatingSubstancesControls.StereoCameraRight)
                        .GetComponent<ColorCorrectionCurves>();
                colorCorrectionCurvesLeft.enabled = true;
                colorCorrectionCurvesRight.enabled = true;
                UiHelper.UpdateToggle(SimulatingSubstancesControls.RedColorToggle);
            }
        }

        private void UpdateRandomEffects(int value)
        {
            if (value > 0)
            {
                var randomization = GameObject.Find(SimulatingSubstancesControls.ARCamera).GetComponent<Randomization>();
                randomization.enabled = true;
                UiHelper.UpdateToggle(SimulatingSubstancesControls.RandomizationToggle);
            }
        }
    }
}