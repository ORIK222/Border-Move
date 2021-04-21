using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SurvivalLevel : MonoBehaviour, ILevelController
{
    public static bool CoolDownIsEnd = false;
    public static bool IsEndGame;

    [SerializeField] private SinglePlayer _player;
    [SerializeField] private SurvivalBorder _border;
    [SerializeField] private TMP_Text _resultCommandText;
    [SerializeField] private TMP_Text _coolDownText;

    private Round _round;
    private CommandsManager.CommandType _lastCommand = CommandsManager.CommandType.OneFingerTap;
   
    public void BeginRound()
    {
        if (IsEndGame)
        {
            return;
        }
        StartCoroutine(BeginRoundCoroutine());
    }
    public IEnumerator BeginRoundCoroutine()
    {
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
        yield return null;
        _round.IsActive = true;

    }
    public void EndRound()
    {
        _round.IsActive = false;
        var winer = CheckRoundResult();
        if (winer)
            _border.IsStop = true;

       _player.MakeRoundResult();
        _round.Count += 1;
        Invoke("BeginRound", 0.4f);
        _player.EndRound();
    }
    public void EndGame()
    {
        IsEndGame = true;
        GameManager.Instance.data.SingleLevelGameCount += 1;
        GameManager.Instance.GameFlow.LooseGame();
    }
    public Player CheckRoundResult ()
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

    private void Awake()
    {
        IsEndGame = false;
        _round = new Round();
    }
    private void Start()
    {
        StartCoroutine("StartLevelCoolDown"); 
        Invoke("BeginRound", 4f);
    }
    private void Update()
    {
        if (!_round.IsActive || IsEndGame)
        {
            return;
        }
        if (_player.GetIsPlayerEnded())
        {
            EndRound();
        }
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
        yield return new WaitForSeconds(0.4f);
        _resultCommandText.text = "";
    }
    private IEnumerator StartLevelCoolDown()
    {
        int timer = 3;
        while (timer >= 0)
        {
            if(timer > 0)
            _coolDownText.text = timer.ToString();
            else
            _coolDownText.text = "GO";
            _coolDownText.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
            LeanTween.value(gameObject, 0.2f, 1f, 0.5f).setOnUpdate((float val) => {
                _coolDownText.transform.localScale = new Vector3(val, val, 1f);
            });
            yield return new WaitForSeconds(1f);
            _coolDownText.text = "";
            timer--;
        }
        CoolDownIsEnd = true;
        yield return null;
    }
}