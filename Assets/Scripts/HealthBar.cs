using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float step;

    public Image sliderIndicator;
    public Slider slider;
    public Sprite[] indicators;

    private void Start()
    {
        step = 1f / indicators.Length;
    }
    private void CalculateStep()
    {
        float percentage = slider.value / slider.maxValue;
        int currentStep = (int) Mathf.Ceil(percentage / step);
        sliderIndicator.sprite = indicators[currentStep - 1];
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        sliderIndicator.sprite = indicators[indicators.Length - 1];
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        CalculateStep();
    }
}
