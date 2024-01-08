using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerTextMinutesSeconds;
    public TMPro.TextMeshProUGUI timerTextMilliseconds;

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

            // Calculate minutes and seconds
            string minutes = ((int)timeElapsed / 60).ToString("00");
            string seconds = (timeElapsed % 60).ToString("00");

            // Display minutes and seconds
            timerTextMinutesSeconds.text = minutes + ":" + seconds;

            // Calculate and display milliseconds
            string milliseconds = ":"+((int)(timeElapsed * 100f) % 100).ToString("00");
            timerTextMilliseconds.text = milliseconds;
        }
    }
}
