using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
   [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (volumeSlider != null && PlayerPrefs.HasKey("Volume"))
        {
           
            LoadVolume();
        }
        else
        {
            volumeSlider.value = 1;
        }
  
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    private void LoadVolume()
    {
        float volume = PlayerPrefs.GetFloat("Volume");
        volumeSlider.value = volume;
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }
}