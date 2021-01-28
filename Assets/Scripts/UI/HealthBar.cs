using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoSingleton<HealthBar>
{
    [SerializeField] Slider slider;
    [SerializeField] Image fillImage;

    [SerializeField] Color maxColor;
    [SerializeField] Color minColor;

    public void UpdateSliderValue(float value)
    {
        slider.value = value;
        fillImage.color = Color.Lerp(minColor, maxColor, value);
    }

}
