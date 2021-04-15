using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SinglePlayer : MonoBehaviour
{    
    public UnityAction<int> RoundWinned;

    [SerializeField] private Multiplier _multiplier;
    private PlayerTaskDisplayer _playerTaskDisplayer;
    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public CommandsManager commandsManager;

    private void Start()
    {
        _playerTaskDisplayer = GetComponent<PlayerTaskDisplayer>();
    }
    public bool GetIsRoundFailed()
    {
        return (!commandsManager.CommandIsRight || commandsManager.Commands.Count > 0);
    }

    public bool GetIsPlayerEnded()
    {
        return (!commandsManager.CommandIsRight || commandsManager.Commands.Count == 0);
    }
    public void StartRound(List<CommandsManager.CommandType> commands, int variation)
    {
        commandsManager.CommandIsRight = true;
        _playerTaskDisplayer.StartRound();
        _playerTaskDisplayer.UpdateInfoPanel(commands, variation);
        commandsManager.StartRound(commands);
    }

    public void MakeRoundResult()
    {
        if (commandsManager.CommandIsRight)
            _playerTaskDisplayer.AchivePositiveEffect();
        else _playerTaskDisplayer.AchiveNegativeEffect();
    }

    public void ScoreAdd(Multiplier.MultiplierType type, int score = 0)
    {
        _multiplier.ChangeMultiplier(type);
        score *= Multiplier.multiplierCount;
        Score += score;
        RoundWinned?.Invoke(Score);
    }

    public void EndRound()
    {
        _playerTaskDisplayer.ClearInfoPanel();
    }
}
