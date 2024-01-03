using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class OptionsData : MonoBehaviour
{            
    public Slider sensitivitySlider;

    void Start()
    {
        LoadSensitivity();
    }

    public void LoadSensitivity()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            float sensitivityValue = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.value = sensitivityValue;
            
        }
    }

    public void SaveSensitivity()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        PlayerPrefs.Save();

    }
}
