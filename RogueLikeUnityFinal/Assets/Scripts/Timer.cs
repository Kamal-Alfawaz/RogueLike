using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText;


    private float startTime;
    private bool isTimerRunning;

    // Start is called before the first frame update
    void Start()
    {
        // Start the timer
        StartTimer();
    }

    public void StartTimer()
    {
        startTime = Time.time;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            float timeElapsed = Time.time - startTime;
            string minutes = ((int)timeElapsed / 60).ToString("00");
            string seconds = (timeElapsed % 60).ToString("00");
            string milliseconds = ((int)(timeElapsed * 100f) % 100).ToString("00");

            timerText.text = minutes + ":" + seconds + ":" + milliseconds;
            if (int.Parse(minutes) == 2){
            timerText.color = Color.red;

        }
        }
        
    }
}


