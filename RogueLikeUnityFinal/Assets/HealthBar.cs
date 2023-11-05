using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        // health bar starts at max health set in unity def
        slider.maxValue = health;
        slider.value = health;
    }

    public void UpdateHealthBar(float currentValue, float maxValue){
    slider.value = currentValue / maxValue;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }

}
