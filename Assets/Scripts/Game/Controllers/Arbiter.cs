using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Arbiter : MonoBehaviour, ILevelController
{

    [SerializeField] private CooperativePlayer _firstPlayer;
    [SerializeField] private CooperativePlayer _secondPlayer;

    [SerializeField] private TimeBarController _timeBar;
    [SerializeField] private TwoPlayerBorderController _border;
    [SerializeField] private RoundCountDisplayer _roundNumberText;
    [SerializeField] private int _maxBorderSteps = 10;
    [SerializeField] private float _roundTime;

    private Round _round;
    private bool _looseGame;
    private int _currentBorderPos;
    private CommandsManager.CommandType _lastCommand = CommandsManager.CommandType.OneFingerTap;
   
    public void BeginRound()
    {
        if (_looseGame)
        {
            return;
        }
        StartCoroutine(BeginRoundCoroutine());
    } 
    public IEnumerator BeginRoundCoroutine()
    {
        _round.Duration = _roundTime;
        _timeBar.SignalGetReady();
        _roundNumberText.RoundCountChange(_round.Count);
        yield return new WaitForSeconds(1f);
        _roundNumberText.ClearText();

        List<CommandsManager.CommandType> nextMoves = new List<CommandsManager.CommandType>();
        for (int i = 0; i < 5; i++)
        {
            nextMoves = CommandsManager.generateMoves(_round.Count);
            if (nextMoves[0] != _lastCommand && nextMoves[0] != CommandsManager.CommandType.TwoFingerTap && nextMoves[0] != CommandsManager.CommandType.TwoFingerLongTap)
            {
                break;
            }
        } // генерація доступних для наступного раунду рухів

        int variation = 1; // один з варіантів завдання для гравця
        if (_round.Count> 10)
        {
            variation = Random.Range(1, 5); //4+1
        }

        _firstPlayer.StartRound(nextMoves, variation);
        _secondPlayer.StartRound(nextMoves, variation);

        _lastCommand = nextMoves[0];
        yield return new WaitForSeconds(0.6f);
        _timeBar.StartRound(_roundTime);
        _round.IsActive = true;
    }    
    public void EndRound()
    {
        _round.IsActive = false;
        var winer = CheckRoundResult();
        if (winer == _firstPlayer)
            MoveBorder(+1);
        else if (winer == _secondPlayer)
            MoveBorder(-1);
        _firstPlayer.MakeRoundResult();
        _secondPlayer.MakeRoundResult();
        _round.Count += 1;

        Invoke("BeginRound", 1f);
        _firstPlayer.EndRound();
        _secondPlayer.EndRound();
    }
    public void EndGame()
    {
        GameManager.Instance.data.CooperativeLevelGameCount += 1;
        GameManager.Instance.GameFlow.LooseGame();
    }
    public Player CheckRoundResult()
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

    private void Awake()
    {
        _round = new Round(_roundTime);
    }

    private void Start()
    {
        BeginRound();
    }

    private void Update()
    {
        if (!_round.IsActive || _looseGame)
        {
            return;
        }

        if (_round.IsActive)
            RoundTimeDecrease();

        if (_firstPlayer.GetIsPlayerEnded() && _secondPlayer.GetIsPlayerEnded())
        {
            EndRound();
        }
    }
    private void MoveBorder(int step)
    {
        _currentBorderPos += step;
        _border.MoveBorder(step);
        if (Mathf.Abs(_currentBorderPos) >= _maxBorderSteps)
        {
            _looseGame = true;
            Invoke("EndGame", 2f);
        }
    }
    private void RoundTimeDecrease()
    {
        _round.Duration -= 1 * Time.deltaTime;
        if (_round.Duration <= 0)
            EndRound();
    }
}