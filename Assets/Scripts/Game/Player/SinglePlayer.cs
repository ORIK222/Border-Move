using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SinglePlayer : Player
{    
    [SerializeField] private Multiplier _multiplier;
    private PlayerTaskDisplayer _playerTaskDisplayer;
    private int _score;

    public int Score
    {
        get { return _score; }
        set { _score = value; }
    }
    public void ScoreAdd(Multiplier.MultiplierType type, int score = 0)
    {
        _multiplier.ChangeMultiplier(type);
        score *= Multiplier.multiplierCount;
        Score += score;
        RoundWinned?.Invoke(Score);
    }
}
