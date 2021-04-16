using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CommandChecker : MonoBehaviour
{

    [SerializeField] private SinglePlayer _player;
    [SerializeField] private TimeBarController _timeBar;
    [SerializeField] private RoundCountDisplayer _roundNumberText;

    [SerializeField] private float _roundTime;
    [SerializeField] private int _scoreNumberForWin = 50000;
    [SerializeField] private TMP_Text _resultRoundText;

    private float _roundEndTimer;
    private int _roundCounter;
    private bool _roundActive;
    private bool _endGame;
    private int _scoreNumber = 100;

    private CommandsManager.CommandType _lastCommand = CommandsManager.CommandType.OneFingerTap;

    private void Awake()
    {
        _roundActive = false;
        _endGame = false;
    }

    private void Start()
    {
        BeginRound();
    }

    private void Update()
    {
        if (!_roundActive || _endGame)
        {
            return;
        }
        if (_player.GetIsPlayerEnded() || Time.time > _roundEndTimer)
        {
            EndRound();
        }
    }

    private void BeginRound()
    {
        if (_endGame)
        {
            return;
        }
        StartCoroutine(BeginRoundCoroutine());
    }

    private IEnumerator BeginRoundCoroutine()
    {
        _timeBar.SignalGetReady();
        _roundNumberText.RoundCountChange(_roundCounter);
        yield return new WaitForSeconds(1f);
        _roundNumberText.ClearText();

        List<CommandsManager.CommandType> nextMoves = new List<CommandsManager.CommandType>();
        for (int i = 0; i < 5; i++)
        {
            nextMoves = CommandsManager.generateMoves(_roundCounter);
            if (nextMoves[0] != _lastCommand && nextMoves[0] != CommandsManager.CommandType.TwoFingerTap && nextMoves[0] != CommandsManager.CommandType.TwoFingerLongTap)
            {
                break;
            }
        } // ãåíåðàö³ÿ äîñòóïíèõ äëÿ íàñòóïíîãî ðàóíäó ðóõ³â

        int variation = 1; // îäèí ç âàð³àíò³â çàâäàííÿ äëÿ ãðàâöÿ
        if (_roundCounter > 10)
        {
            variation = Random.Range(1, 5); //4+1
        }

        _player.StartRound(nextMoves, variation);
        _lastCommand = nextMoves[0];
        yield return new WaitForSeconds(0.6f);
        _timeBar.StartRound(_roundTime);
        _roundEndTimer = Time.time + _roundTime;
        _roundActive = true;
    }

    private void EndRound()
    {
        _roundActive = false;
        var winer = SelectRoundWinner();
        if (!winer)
        {
            Equality();
        }
        else
        {
            if (winer)
                CheckScoreCount();
            _player.MakeRoundResult();
        }
        _roundCounter += 1;
        _roundTime -= _roundTime / 50;
        Invoke("BeginRound", 1f);
        _player.EndRound();
    }

    private void CheckScoreCount()
    {
        if (_player.Score >= _scoreNumberForWin)
        {
            _endGame = true;
            Invoke("EndGame", 2f);
        }
    }

    private void EndGame()
    {
        GameManager.Instance.GameFlow.LooseGame();
    }

    private SinglePlayer SelectRoundWinner()
    {
        SinglePlayer winer = null;
        bool isWin = !_player.GetIsRoundFailed();
        if (isWin)
        {
            _player.ScoreAdd(_timeBar.GetMultiplierTypeDependingOnTheReaction(), _scoreNumber);
            StartCoroutine(ShowResultRoundText("Excellent", isWin));
            winer = _player;
        }
        else
        {
            StartCoroutine(ShowResultRoundText("Mistake", isWin));
            _player.ScoreAdd(Multiplier.MultiplierType.Wrong);
        }
        return winer;
    }
    private IEnumerator ShowResultRoundText(string result, bool isWin)
    {
        if (isWin)
            _resultRoundText.color = Color.green;
        else _resultRoundText.color = Color.red;

        _resultRoundText.text = result;
        _resultRoundText.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        //_resultRoundText.text = Localer.GetText(result);
        LeanTween.value(gameObject, 0.2f, 1f, 0.5f).setOnUpdate((float val) => {
            _resultRoundText.transform.localScale = new Vector3(val, val, 1f);
        });
        yield return new WaitForSeconds(0.7f);
        _resultRoundText.text = "";
    }
    private void Equality()
    {
        Debug.Log("!Equality!");
    }


}