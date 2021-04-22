using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsManager : MonoBehaviour
{
    public enum CommandType
    {
        Empty = 0,
        OneFingerTap,
        OneFingerLongTap,
        TwoFingerTap,
        GeneralFlick,
        LeftFlick,
        RightFlick,
        UpFlick,
        DownFlick,
        TwoFingerLongTap,
    }
    public Queue<CommandType> Commands;
    public bool CommandIsRight;
    
    public float LastActionTime
    {
        get { return _lastActionTime; }
        set { _lastActionTime = value; }
    }
    private float _lastActionTime;

    public void StartRound(List<CommandType> commands)
    {
        Commands = new Queue<CommandType>();
        foreach (var command in commands)
        {
            Commands.Enqueue(command);
        }
    }
    public void CheckCurrentCommand(CommandType currentCommand)
    {
        LastActionTime = Time.time;
        if (Commands.Count > 0)
        {
            CommandType cmd = Commands.Dequeue();
            if (cmd == CommandType.GeneralFlick)
            {
                CommandIsRight = (currentCommand == CommandType.UpFlick ||
                                currentCommand == CommandType.DownFlick ||
                                currentCommand == CommandType.LeftFlick ||
                                currentCommand == CommandType.RightFlick);
            }
            else
            {
                CommandIsRight = (currentCommand == cmd);
            }
        }
        else
        {
            CommandIsRight = false;
        }
    }
    public static List<CommandType> generateMoves(int roundCounter)
    {
        List<CommandType> nextTask = new List<CommandType>();
        if (roundCounter < 3)
        {
            var com = getRandomCommand(CommandType.OneFingerTap, CommandType.GeneralFlick);
            nextTask = GetRandomComList(com, 3);
        }
        else if (roundCounter < 9)
        {
            var com = getRandomCommand(CommandType.OneFingerTap, CommandType.GeneralFlick);
            nextTask = GetRandomComList(com, 3);
        }
        else
        {
            var com = getRandomCommand(CommandType.OneFingerTap, CommandType.TwoFingerLongTap);
            nextTask = GetRandomComList(com, 4);
        }
        return nextTask;
    }

    private static CommandType getRandomCommand(CommandType from, CommandType to)
    {
        return (CommandType)Random.Range((int)from, (int)to + 1);
    }
    private static List<CommandType> GetRandomComList(CommandType com, int maxI)
    {
        List<CommandType> list = new List<CommandType>();
        int comCount = 1;
        if (com == CommandType.OneFingerTap)
        {
            comCount = Random.Range(1, maxI);
        }
        for (int i = 0; i < comCount; i++)
        {
            list.Add(com);
        }
        return list;
    }
}