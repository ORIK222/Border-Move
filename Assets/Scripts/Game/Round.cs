using UnityEngine;

public class Round
{ 
    public int Count;
    public bool IsActive;
    public float Duration;
    public Round()
    {
        Count = 0;
        IsActive = false;
        Duration = 0;
    }

    public Round(float duration)
    {
        Duration = duration;
        Count = 0;
        IsActive = false;
    }
}
