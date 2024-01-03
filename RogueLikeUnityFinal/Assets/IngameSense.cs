using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class IngameSense : MonoBehaviour
{
   [SerializeField] Slider SenSlider;


    // Start is called before the first frame update
    void Start()
    {
        if (SenSlider != null && PlayerPrefs.HasKey("Sensetivity"))
        {
           
            LoadSense();
        }
      
        else
        {
            SenSlider.value = 50;
        }
  
    }

    public void ChangeSensetivity()
    {

        PlayerPrefs.SetFloat("Sensetivity", SenSlider.value);

    }

    private void LoadSense()
    {
        float sensitivityValue = PlayerPrefs.GetFloat("Sensetivity");
        SenSlider.value = sensitivityValue;
    }

}