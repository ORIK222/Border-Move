using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static string _currentTime;
    public static string CurrentTime
    {
        get => _currentTime;
        private set { }
    }
    private TMP_Text _timeText;
    private float _timeBeforeSeconds = 0;
    [SerializeField] private int _gameDuration = 0;

    private void Start()
    {
        _timeText = GetComponent<TMP_Text>();
        _timeText.text = Localer.GetText("Time");
    }

    private void FixedUpdate()
    {
        if(SurvivalLevel.CoolDownIsEnd && !SurvivalLevel.IsEndGame)
        CalculationTime();
    }

    private void CalculationTime()
    {
        _timeBeforeSeconds += 1 * Time.deltaTime;
        if (_timeBeforeSeconds >= 1)
        {
            _gameDuration += 1;
            _timeBeforeSeconds = 0;
            _timeText.text = Localer.GetText("Time") + ": " + SecondsToMinutesConverter(_gameDuration);
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
            _currentTime = timeInString;
            return timeInString;
        }
    }
}
