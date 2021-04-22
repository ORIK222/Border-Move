using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static string CurrentTime
    {
        get => _currentTime;
        private set { }
    }
    
    [SerializeField] private int _gameDuration = 0;
    private static string _currentTime;
    private TMP_Text _timeText;
    private float _timeBeforeSeconds = 0;

    private void Start()
    {
        _timeText = GetComponent<TMP_Text>();
        _timeText.text = Localer.GetText("Time");
    }
    private void FixedUpdate()
    {
        if(SurvivalLevel.CoolDownIsEnd && !SurvivalLevel.IsEndGame)
        CalculationTime();
        if (SurvivalLevel.IsEndGame)
            CheckGameResult();
    }
    private void CheckGameResult()
    {
        if (_gameDuration > GameManager.Instance.data.SurvivalLevelTime)
        {
            GameManager.Instance.data.SurvivalLevelTime = _gameDuration;
            GameManager.Instance.data.SurvivalLevelTimeInString = _currentTime;
        }
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
        {
            if(timeInSeconds < 10)
                _currentTime = "0:0" + timeInSeconds.ToString();
            else
                _currentTime = "0:" + timeInSeconds.ToString();
            return _currentTime;
        }
        else
        {
            minutes = timeInSeconds / 60;
            seconds = timeInSeconds % 60;

            if (seconds >= 10)
                timeInString = minutes.ToString() + ":" + seconds;
            else
                timeInString = minutes.ToString() + ":0" + seconds;
            _currentTime = timeInString;
            return _currentTime;
        }
    }
}
