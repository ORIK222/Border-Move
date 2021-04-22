using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreLevel : MonoBehaviour, ILevelController
{
    public UnityAction OnRoundLoseEvent;

    [SerializeField] private SinglePlayer _player;
    [SerializeField] private TimeBarController _timeBar;
    [SerializeField] private RoundCountDisplayer _roundNumberText;

    [SerializeField] private float _roundTime;
    [SerializeField] private TMP_Text _resultRoundText;

    private Round _round;
    private bool _endGame;
    private int _scoreNumber = 100;

    private CommandsManager.CommandType _lastCommand = CommandsManager.CommandType.OneFingerTap;
    
    public void BeginRound()
    {
        if (_endGame)
        {
            return;
        }
        StartCoroutine(BeginRoundCoroutine());
    }
    public IEnumerator BeginRoundCoroutine()
    {
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
        }

        int variation = 1; 
        if (_round.Count > 10)
        {
            variation = Random.Range(1, 5); //4+1
        }

        _player.StartRound(nextMoves, variation);
        _lastCommand = nextMoves[0];
        yield return new WaitForSeconds(0.6f);
        _timeBar.StartRound(_roundTime);
        _round.IsActive = true;
    }
    public void EndRound()
    {
        _round.IsActive = false;
        var winer = CheckRoundResult();
        _player.MakeRoundResult();
        _round.Count += 1;
        _roundTime -= _roundTime / 50;
        _round.Duration = _roundTime;        
        _player.EndRound();
        if(!_endGame)
        Invoke("BeginRound", 1f);

    }    
    public void EndGame()
    {
        GameManager.Instance.data.SingleLevelGameCount += 1;
        if(_player.Score > GameManager.Instance.data.ScoreLevelResult)
        GameManager.Instance.data.ScoreLevelResult = _player.Score;
        GameManager.Instance.GameFlow.LooseGame();
        Heart.isEmpty = false;
    }
    public Player CheckRoundResult()
    {
        Debug.Log("CheckRoundResult");
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
            OnRoundLoseEvent?.Invoke();
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
        if (!_round.IsActive || _endGame)
        {
            return;
        }
        if (_player.GetIsPlayerEnded())
        {
            EndRound();
        }
        if (_round.IsActive)
            RoundTimeDecrease();

        if (Heart.isEmpty)
            Invoke("EndGame", 1f);
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

    private void RoundTimeDecrease()
    {
        _round.Duration -= 1 * Time.deltaTime;
        if (_round.Duration <= 0)
            EndRound();
    }
}