using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Helpers
{
    public static class UiHelper
    {
        public static void UpdateDropdown(string dropdownName, string playerPrefName)
        {
            var dropdown = GameObject.Find(dropdownName).GetComponent<Dropdown>();
            dropdown.value = PlayerPrefs.GetInt(playerPrefName);
        }

        public static void UpdateSlider(string sliderName, string playerPrefName)
        {
            var slider = GameObject.Find(sliderName).GetComponent<Slider>();
            slider.value = PlayerPrefs.GetFloat(playerPrefName);
        }

        public static void UpdateToggle(string toggleName)
        {
            var toggle = GameObject.Find(toggleName).GetComponent<Toggle>();
            toggle.isOn = true;
        }

        public static void SetEffectValueText<T>(T value, string textFieldName)
        {
            var textField = GameObject.Find(textFieldName).GetComponent<Text>();
            textField.text = value.ToString();
        }

        public static void ResetDropdowns()
        {
            var dropdowns = Object.FindObjectsOfType<Dropdown>();
            foreach (var dropdown in dropdowns)
            {
                dropdown.value = 0;
            }
        }

        public static void ResetSliders()
        {
            var sliders = Object.FindObjectsOfType<Slider>();
            foreach (var slider in sliders)
            {
                slider.value = 0;
            }
        }
    }
}