using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arbiter : MonoBehaviour
{

    [SerializeField] private CooperativePlayer _firstPlayer;
    [SerializeField] private CooperativePlayer _secondPlayer;

    [SerializeField] private TimeBarController _timeBar;
    [SerializeField] private BorderController _border;
    [SerializeField] private RoundCountDisplayer _roundNumberText;
    [SerializeField] private float RoundTime;
    [SerializeField] private int MaxBorderSteps = 10;

    private float RoundEndTimer;
    private int _roundCounter;
    private bool _roundActive;
    private bool _looseGame;
    private int _currentBorderPos;
    private CommandsManager.CommandType _lastCommand = CommandsManager.CommandType.OneFingerTap;

    private void Awake()
    {
        _roundActive = false;
        _looseGame = false;
    }

    private void Start()
    {
        BeginRound();
    }

    private void Update()
    {
        if (!_roundActive || _looseGame)
        {
            return;
        }
        if (_firstPlayer.GetIsPlayerEnded() && _secondPlayer.GetIsPlayerEnded())
        {
            EndRound();
        }

        if (Time.time > RoundEndTimer)
        {
            EndRound();
        }
    }

    private void BeginRound()
    {
        if (_looseGame)
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
            if (nextMoves[0] != _lastCommand)
            {
                break;
            }
        } // генерація доступних для наступного раунду рухів

        int variation = 1; // один з варіантів завдання для гравця
        if (_roundCounter > 10)
        {
            variation = Random.Range(1, 5); //4+1
        }

        _firstPlayer.StartRound(nextMoves, variation);
        _secondPlayer.StartRound(nextMoves, variation);

        _lastCommand = nextMoves[0];
        yield return new WaitForSeconds(0.6f);
        _timeBar.StartRound(RoundTime);
        RoundEndTimer = Time.time + RoundTime;
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
            if (winer == _firstPlayer)
                MoveBorder(+1);
            else if (winer == _secondPlayer)
                MoveBorder(-1);
            else
                Equality();

            _firstPlayer.MakeRoundResult();
            _secondPlayer.MakeRoundResult();
        }
        _roundCounter += 1;
        //todo гавнокод
        Invoke("BeginRound", 1f);
        _firstPlayer.EndRound();
        _secondPlayer.EndRound();
        //
    }

    private void MoveBorder(int step)
    {
        _currentBorderPos += step;
        _border.MoveBorder(step);
        if (Mathf.Abs(_currentBorderPos) >= MaxBorderSteps)
        {
            _looseGame = true;
            Invoke("LooseGame", 2f);
        }
    }

    private void LooseGame()
    {
        GameManager.Instance.GameFlow.LooseGame();
    }

    private CooperativePlayer SelectRoundWinner()
    {
        CooperativePlayer winer = null;
        bool p0Win = !_firstPlayer.GetIsRoundFailed();
        bool p1Win = !_secondPlayer.GetIsRoundFailed();
        if (p0Win && p1Win)
        {
            float time0 = _firstPlayer.commandsManager.LastActionTime;
            float time1 = _secondPlayer.commandsManager.LastActionTime;
            winer = time0 < time1 ? _firstPlayer : _secondPlayer;
        }
        else if (p0Win || p1Win)
        {
            winer = (p0Win) ? _firstPlayer : _secondPlayer;
        }
        return winer;
    }
    private void Equality()
    {
        Debug.Log("!Equality!");
    }


}

// CHEAT
/*
i = Random.Range(0, 2);
if (i == 0)
{
    nextMoves = new List<CommandsManager.CommandType>();
    nextMoves.Add(CommandsManager.CommandType.OneFingerTap);
} else {
    nextMoves = new List<CommandsManager.CommandType>();
    nextMoves.Add(CommandsManager.CommandType.TwoFingerTap);            
}
*/
//