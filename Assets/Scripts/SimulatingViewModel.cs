using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class SimulatingViewModel : MonoBehaviour
{
    private void Start()
    {
        UpdateBlurValue(PlayerPrefs.GetFloat("BlurLevel"));
        UpdateTunnelValue(PlayerPrefs.GetFloat("TunnelLevel"));
        UpdateDelay(PlayerPrefs.GetInt("DelayLevel"));
        UpdateMotionBlur(PlayerPrefs.GetInt("MotionBlur"));
        UpdateRedColorDistortion(PlayerPrefs.GetInt("RedColorDistortion"));
        UpdateRandomEffects(PlayerPrefs.GetInt("RandomEffects"));
    }

    private void UpdateBlurValue(float value)
    {
        if (value > 0)
        {
            var blurValueText = GameObject.Find("BlurLevelText").GetComponent<Text>();
            blurValueText.text = value.ToString();
            var cameraLeftBlur = GameObject.Find("StereoCameraLeft").GetComponent<BlurOptimized>();
            var cameraRightBlur = GameObject.Find("StereoCameraRight").GetComponent<BlurOptimized>();
            cameraLeftBlur.blurSize = value;
            cameraRightBlur.blurSize = value;
            UpdateToggle("BlurToggle");
        }
    }

    private void UpdateTunnelValue(float value)
    {
        if (value > 0)
        {
            var tunnelValueText = GameObject.Find("TunnelLevelText").GetComponent<Text>();
            tunnelValueText.text = value.ToString();
            var cameraLeftTunnel = GameObject.Find("StereoCameraLeft").GetComponent<AlcoholTiltShift>();
            var cameraRightTunnel = GameObject.Find("StereoCameraRight").GetComponent<AlcoholTiltShift>();
            cameraLeftTunnel.blurArea = value;
            cameraRightTunnel.blurArea = value;
            UpdateToggle("TunnelToggle");
        }
    }

    private void UpdateDelay(float value)
    {
        if (value > 0)
        {
            var delayLeft = GameObject.Find("StereoCameraLeft").GetComponent<Delay>();
            var delayRight = GameObject.Find("StereoCameraRight").GetComponent<Delay>();
            delayLeft.enabled = true;
            delayRight.enabled = true;
            UpdateToggle("DelayToggle");
        }
    }

    private void UpdateMotionBlur(int value)
    {
        if (value > 0)
        {
            var motionBlurLeft = GameObject.Find("StereoCameraLeft").GetComponent<CameraMotionBlur>();
            var motionBlurRight = GameObject.Find("StereoCameraRight").GetComponent<CameraMotionBlur>();
            motionBlurLeft.enabled = true;
            motionBlurRight.enabled = true;
            UpdateToggle("MotionBlurToggle");
        }
    }

    private void UpdateRedColorDistortion(int value)
    {
        if (value > 0)
        {
            var colorCorrectionCurvesLeft = GameObject.Find("StereoCameraLeft").GetComponent<ColorCorrectionCurves>();
            var colorCorrectionCurvesRight = GameObject.Find("StereoCameraRight").GetComponent<ColorCorrectionCurves>();
            colorCorrectionCurvesLeft.enabled = true;
            colorCorrectionCurvesRight.enabled = true;
            UpdateToggle("RedColorToggle");
        }
    }

    private void UpdateRandomEffects(int value)
    {
        if (value > 0)
        {
            var randomization = GameObject.Find("ARCamera").GetComponent<Randomization>();
            randomization.enabled = true;
            UpdateToggle("RandomToggle");
        }
    }

    private void UpdateToggle(string toggleName)
    {
        var toggle = GameObject.Find(toggleName).GetComponent<Toggle>();
        toggle.isOn = true;
    }
}