using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TMP_Text _timeText;
    private float _timeBeforeSeconds = 0;
    [SerializeField] private int _gameDuration = 0;

    private void Start()
    {
        _timeText = GetComponent<TMP_Text>();
    }

    private void FixedUpdate()
    {
        CalculationTime();
    }

    private void CalculationTime()
    {
        _timeBeforeSeconds += 1 * Time.deltaTime;
        if (_timeBeforeSeconds >= 1)
        {
            _gameDuration += 1;
            _timeBeforeSeconds = 0;
            _timeText.text = "Time: " + SecondsToMinutesConverter(_gameDuration);

        }
    }

    private string SecondsToMinutesConverter(int timeInSeconds)
    {
        string timeInString;
        int seconds, minutes;
        if (timeInSeconds <= 60)
            return timeInSeconds.ToString();
        else
        {
            minutes = timeInSeconds / 60;
            seconds = timeInSeconds % 60;
            if(seconds >= 10)
                timeInString = minutes.ToString() + ":" + seconds;
            else
                timeInString = minutes.ToString() + ":0" + seconds;
            return timeInString;
        }
    }
}
