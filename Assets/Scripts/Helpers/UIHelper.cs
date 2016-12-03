using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Helpers
{
    public static class UiHelper
    {
        public static void UpdateDropdown(string dropdownName, string playerPrefName)
        {
            Dropdown dropdown = GameObject.Find(dropdownName).GetComponent<Dropdown>();
            dropdown.value = PlayerPrefs.GetInt(playerPrefName);
        }

        public static void UpdateSlider(string sliderName, string playerPrefName)
        {
            Slider slider = GameObject.Find(sliderName).GetComponent<Slider>();
            slider.value = PlayerPrefs.GetFloat(playerPrefName);
        }

        public static void UpdateToggle(String toggleName)
        {
            Toggle toggle = GameObject.Find(toggleName).GetComponent<Toggle>();
            toggle.isOn = true;
        }

        public static void SetEffectValueText<T>(T value, String textFieldName)
        {
            Text textField = GameObject.Find(textFieldName).GetComponent<Text>();
            textField.text = value.ToString();
        }

        public static void ResetDropdowns()
        {
            Dropdown[] dropdowns = GameObject.FindObjectsOfType<Dropdown>();
            foreach (Dropdown dropdown in dropdowns)
            {
                dropdown.value = 0;
            }
        }

        public static void ResetSliders()
        {
            Slider[] sliders = GameObject.FindObjectsOfType<Slider>();
            foreach (Slider slider in sliders)
            {
                slider.value = 0;
            }
        }
    }
}
