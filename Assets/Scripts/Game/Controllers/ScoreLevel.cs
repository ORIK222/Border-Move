using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreLevel : MonoBehaviour, ILevelController
{

    [SerializeField] private SinglePlayer _player;
    [SerializeField] private TimeBarController _timeBar;
    [SerializeField] private RoundCountDisplayer _roundNumberText;

    [SerializeField] private float _roundTime;
    [SerializeField] private int _scoreNumberForWin = 50000;
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
        if (winer)
            CheckScoreCount();

        _player.MakeRoundResult();
        _round.Count += 1;
        _roundTime -= _roundTime / 50;
        Invoke("BeginRound", 1f);
        _player.EndRound();
    }    
    public void EndGame()
    {
        GameManager.Instance.data.SingleLevelGameCount += 1;
        if (_scoreNumber > GameManager.Instance.data.ScoreLevelResult)
            GameManager.Instance.data.ScoreLevelResult = _scoreNumber;
        GameManager.Instance.GameFlow.LooseGame();
    }
    public Player CheckRoundResult()
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
    }

    private void CheckScoreCount()
    {
        if (_player.Score >= _scoreNumberForWin)
        {
            _endGame = true;
            Invoke("EndGame", 2f);
        }
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