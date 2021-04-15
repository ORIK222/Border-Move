using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CooperativePlayer : MonoBehaviour
{    
    public UnityAction<int> RoundWinned;
    private PlayerTaskDisplayer _playerTaskDisplayer;

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

    public void EndRound()
    {
        _playerTaskDisplayer.ClearInfoPanel();
    }
}
