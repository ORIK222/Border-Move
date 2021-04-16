using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SurvivalLevel : MonoBehaviour
{

    [SerializeField] private SinglePlayer _player;
    [SerializeField] private SurvivalBorder _border;
    [SerializeField] private TMP_Text _resultCommandText;

    private int _roundCounter;
    private bool _endGame;

    private CommandsManager.CommandType _lastCommand = CommandsManager.CommandType.OneFingerTap;

    private void Awake()
    {
        _endGame = false;
    }

    private void Start()
    {
        BeginRound();
    }

    private void Update()
    {
        if (_endGame)
        {
            return;
        }
        if (_player.GetIsPlayerEnded())
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
        List<CommandsManager.CommandType> nextMoves = new List<CommandsManager.CommandType>();
        for (int i = 0; i < 5; i++)
        {
            nextMoves = CommandsManager.generateMoves(_roundCounter);
            if (nextMoves[0] != _lastCommand && nextMoves[0] != CommandsManager.CommandType.TwoFingerTap && nextMoves[0] != CommandsManager.CommandType.TwoFingerLongTap)
            {
                break;
            }
        }

        int variation = 1;
        if (_roundCounter > 10)
        {
            variation = Random.Range(1, 5); //4+1
        }

        _player.StartRound(nextMoves, variation);
        _lastCommand = nextMoves[0];
        yield return new WaitForSeconds(0.2f);
    }

    private void EndRound()
    {
        SinglePlayer winer = CheckRoundResult();
        if (winer)
            _border.IsStop = true;
       _player.MakeRoundResult();
        _roundCounter += 1;
        Invoke("BeginRound", 1f);
        _player.EndRound();
    }

    private void CheckBorderPosition()
    {
        if (!_border)
        {
            _endGame = true;
            Invoke("EndGame", 2f);
        }
    }

    private void EndGame()
    {
        GameManager.Instance.GameFlow.LooseGame();
    }

    private SinglePlayer CheckRoundResult ()
    {
        SinglePlayer winer = null;
        bool isWin = !_player.GetIsRoundFailed();
        if (isWin)
        {
            StartCoroutine(ShowResultRoundText("Excellent", isWin));
            winer = _player;
        }
        else
        {
            StartCoroutine(ShowResultRoundText("Mistake", isWin));
        }
        return winer;
    }

    private IEnumerator ShowResultRoundText(string result, bool isWin)
    {
        if (isWin)
            _resultCommandText.color = Color.green;
        else _resultCommandText.color = Color.red;

        _resultCommandText.text = result;
        _resultCommandText.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        //_resultRoundText.text = Localer.GetText(result);
        LeanTween.value(gameObject, 0.2f, 1f, 0.5f).setOnUpdate((float val) => {
            _resultCommandText.transform.localScale = new Vector3(val, val, 1f);
        });
        yield return new WaitForSeconds(0.7f);
        _resultCommandText.text = "";
    }
}