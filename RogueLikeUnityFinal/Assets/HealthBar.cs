using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private Gradient colorGradient;

    private float lerpspeed = 0.25f;
    private float time;


    public void UpdateHealthBar(float currentValue, float maxValue){
        time = 0;
        AnimateHealthBar();

        // Round the values
        float roundedCurrentValue = Mathf.Round(currentValue);
        float roundedMaxValue = Mathf.Round(maxValue);

        slider.value = currentValue / maxValue;
        healthText.text = "Health: " + roundedCurrentValue.ToString() + "/" + roundedMaxValue.ToString();
        _healthBarFill.color = colorGradient.Evaluate(slider.value);
    }

    
        private void AnimateHealthBar(){
        float currentValue = slider.value;
        float targetValue = currentValue;
        time += Time.deltaTime * lerpspeed;
        slider.value = Mathf.Lerp(currentValue, targetValue, time);
        }
}